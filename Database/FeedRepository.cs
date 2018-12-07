using Microsoft.EntityFrameworkCore;
using ModusCreate.NewsFeed.Domain;
using System;
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

		public async Task<IEnumerable<FeedSubscription>> GetAll() => await this.dbContext.FeedSubscriptions.ToListAsync();
	}
}
