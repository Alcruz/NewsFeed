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
		public DbSet<Feed> Feeds { get; set; }
		public DbSet<FeedEntry> FeedEntries { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<FeedSubscription>()
				.HasOne(feedSubscription => feedSubscription.Feed).WithOne().HasForeignKey<Feed>(feed => feed.Id);

			modelBuilder.Entity<FeedEntry>()
				.HasKey(p => new { p.FeedId, p.Id });
		}
	}
}
