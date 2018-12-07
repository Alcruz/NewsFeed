using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModusCreate.NewsFeed.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ModusCreate.NewsFeed.Web.Background
{
	public class DownloadFeedHostedService : IHostedService, IDisposable
	{
		private readonly IServiceProvider services;
		private Timer timer;

		public DownloadFeedHostedService(IServiceProvider services)
		{
			this.services = services;
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			if (timer != null)
				this.timer = new Timer(async state => await DoWork(), null, TimeSpan.Zero, TimeSpan.FromMinutes(2));

			return Task.CompletedTask;
		}

		public async Task DoWork()
		{
			using (var scope = services.CreateScope())
			{
				var service =
					scope.ServiceProvider
						.GetRequiredService<FeedService>();

				await service.UpdateFeedCache();
			}
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			this.timer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			this.timer?.Dispose();
		}
	}
}
