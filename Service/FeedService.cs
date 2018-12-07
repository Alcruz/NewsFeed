using ModusCreate.NewsFeed.Database;
using ModusCreate.NewsFeed.Domain;
using System;
using System.IO;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

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

			if (Feed.IsValid(feedUrl))
				throw new FeedException("Invalid feed url or feed content is not supported");

			var feed = new Feed(feedUrl);
			var subscription = feed.SubscribeTo();

			await feedRepository.AddSubscription(subscription);

			return subscription;
		}
	}
}
