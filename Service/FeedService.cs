using ModusCreate.NewsFeed.Database;
using ModusCreate.NewsFeed.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModusCreate.NewsFeed.Service
{
	public class FeedService
	{
		private readonly FeedRepository feedRepository;

		public FeedService(FeedRepository feedRepository)
		{
			this.feedRepository = feedRepository;
		}

		public async Task<FeedSubscription> Subscribe(string feedUrl)
		{
			if (feedUrl == null)
				throw new ArgumentNullException(nameof(feedUrl));

			if (!await RssFeed.IsValid(feedUrl))
				throw new FeedException("Invalid feed url or feed content is not supported");

			var feed = await RssFeed.CreateFromUrl(feedUrl);
			var subscription = feed.SubscribeTo();

			await feedRepository.AddSubscription(subscription);

			return subscription;
		}

		public async Task<IEnumerable<FeedSubscription>> GetAll() => await this.feedRepository.GetAll();

		public async Task<FeedSubscription> GetSubscription(int id) => await this.feedRepository.Find(id);

		public async Task<IEnumerable<FeedEntry>> GetAllFeedEntries() => await this.feedRepository.GetAllEntires();

		public async Task UpdateFeedCache()
		{
			foreach (var subscription in await this.feedRepository.GetAll())
			{
				var rssFeed = await RssFeed.CreateFromUrl(subscription.Feed.FeedUrl);
				var entries = rssFeed.Entries;
				foreach (var entry in entries)
				{
					entry.FeedId = subscription.Feed.Id;
				}
				await this.feedRepository.UpdateFeedEntries(entries);
			}
		}

        public async Task DeleteSubscription(int id) => await this.feedRepository.DeleteSuscription(id);
    }
}
