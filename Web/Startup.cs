using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModusCreate.NewsFeed.Database;
using ModusCreate.NewsFeed.Domain;
using ModusCreate.NewsFeed.Service;
using ModusCreate.NewsFeed.Web.Background;
using System.Linq;

namespace ModusCreate.NewsFeed.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});

			DatabaseModule.Register(services, Configuration.GetConnectionString("DefaultConnection"), GetType().Assembly.FullName);
			ServiceModule.Register(services);

			services.AddHostedService<DownloadFeedHostedService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			UpdateDatabase(app);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				// To learn more about options for serving an Angular SPA from ASP.NET Core,
				// see https://go.microsoft.com/fwlink/?linkid=864501

				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseAngularCliServer(npmScript: "start");
				}
			});
		}

		private static void UpdateDatabase(IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices
				.GetRequiredService<IServiceScopeFactory>()
				.CreateScope())
			{
				using (var context = serviceScope.ServiceProvider.GetService<AppDbContext>())
				{
					context.Database.Migrate();
					if (context.FeedSubscriptions.Any())
						return;

					var feed1 = RssFeed.CreateFromUrl("http://newsrss.bbc.co.uk/rss/newsonline_world_edition/americas/rss.xml").GetAwaiter().GetResult().SubscribeTo();
					var feed2 = RssFeed.CreateFromUrl("https://www.ed.gov/feed").GetAwaiter().GetResult().SubscribeTo();
					var feed3 = RssFeed.CreateFromUrl("http://feeds1.nytimes.com/nyt/rss/Sports").GetAwaiter().GetResult().SubscribeTo();
					context.FeedSubscriptions.AddRange(new[] { feed1, feed2, feed3 });
					context.SaveChanges();
				}
			}
		}
	}
}
