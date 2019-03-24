using Microsoft.EntityFrameworkCore;
using ModusCreate.NewsFeed.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModusCreate.NewsFeed.Database
{
	public class FeedRepository
	{
		private readonly AppDbContext dbContext;

		public FeedRepository(AppDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddSubscription(FeedSubscription feedSubscription)
		{
			if (feedSubscription == null)
				throw new ArgumentNullException(nameof(feedSubscription));

			this.dbContext.FeedSubscriptions.Add(feedSubscription);
			await this.dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<FeedSubscription>> GetAll() => await this.dbContext.FeedSubscriptions.Include(fs => fs.Feed).ToListAsync();

		public async Task<FeedSubscription> Find(int id)
		{
			var subscription = await this.dbContext.FeedSubscriptions.FindAsync(id);
			await this.dbContext.Entry(subscription).Reference(s => s.Feed).LoadAsync();
			await this.dbContext.Entry(subscription.Feed).Collection(p => p.Entries).LoadAsync();
			return subscription;
		}
		public async Task UpdateFeedEntries(List<FeedEntry> entries)
		{
			try
			{
				this.dbContext.FeedEntries.UpdateRange(entries);
				await this.dbContext.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				foreach (var data in ex.Entries)
					data.OriginalValues.SetValues(data.GetDatabaseValues());
			}
		}

		public async Task<IEnumerable<FeedEntry>> GetAllEntires()
		{
			return await this.dbContext.FeedEntries.OrderBy(fe => fe.FeedId).OrderByDescending(fe => fe.PublishDate).ToListAsync();
		}

        public async Task DeleteSuscription(int id)
        {
            var subscription = await this.dbContext.FeedSubscriptions.FindAsync(id);
            this.dbContext.FeedSubscriptions.Remove(subscription);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
