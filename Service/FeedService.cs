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

			if (!await Feed.IsValid(feedUrl))
				throw new FeedException("Invalid feed url or feed content is not supported");

			var feed = await Feed.CreateFeedFromUrl(feedUrl);
			var subscription = feed.SubscribeTo();

			await feedRepository.AddSubscription(subscription);

			return subscription;
		}

		public async Task<IEnumerable<FeedSubscription>> GetAll() => await this.feedRepository.GetAll();

		public async Task<FeedSubscription> GetSubscription(int id) => await this.feedRepository.Find(id);
	}
}
