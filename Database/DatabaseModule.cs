using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ModusCreate.NewsFeed.Database
{
	public class DatabaseModule
	{
		public static void Register(IServiceCollection services, string connection, string migrationsAssembly)
		{
			services.AddDbContext<AppDbContext>(options =>
					options.UseSqlServer(connection, builder => builder.MigrationsAssembly(migrationsAssembly)));

			services.AddScoped<FeedRepository>();
		}
	}
}
