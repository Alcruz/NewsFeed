using Microsoft.EntityFrameworkCore;
using ModusCreate.NewsFeed.Domain;

namespace ModusCreate.NewsFeed.Database
{
    public class AppDbContext : DbContext 
    {
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<FeedSubscription> FeedSubscriptions { get; set; }
	}
}
