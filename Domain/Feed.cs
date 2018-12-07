using System;
using System.ServiceModel.Syndication;
using System.Xml;

namespace ModusCreate.NewsFeed.Domain
{
	public class Feed
	{
		private SyndicationFeed feed;

		public Feed(string feedUrl)
		{
			FeedUrl = feedUrl;
			this.feed = ParseFeed(feedUrl);
		}

		public string FeedUrl { get; }

		public string Title => this.Title;

		public static bool IsValid(string feedUrl)
		{
			try
			{
				ParseFeed(feedUrl);
				return true;
			}
			catch
			{
				return false;
			}
		}

		private static SyndicationFeed ParseFeed(string feedUrl)
		{
			using (XmlReader reader = XmlReader.Create(feedUrl))
			{
				return SyndicationFeed.Load(reader);
			}
		}

		public FeedSubscription SubscribeTo()
		{
			return new FeedSubscription
			{
				Title = Title,
				FeedUrl = FeedUrl
			};
		}
	}

	public class FeedException : Exception
	{
		public FeedException(string message) : base(message)
		{
		}
	}
}
