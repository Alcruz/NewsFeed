using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModusCreate.NewsFeed.Service
{
	public class ServiceModule
	{
		public static void Register(IServiceCollection services)
		{
			services.AddScoped<FeedService>();
		}
	}
}
