using System;
using System.IO;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace ModusCreate.NewsFeed.Domain
{
	public class Feed
	{
		private SyndicationFeed feed;

		private Feed(string feedUrl, SyndicationFeed feed)
		{
			FeedUrl = feedUrl;
			this.feed = feed;
		}

		public string FeedUrl { get; }

		public string Title => this.feed.Title.Text;

		public static async Task<bool> IsValid(string feedUrl)
		{
			try
			{
				await ParseFeed(feedUrl);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static async Task<Feed> CreateFeedFromUrl(string feedUrl) => new Feed(feedUrl, await ParseFeed(feedUrl));

		private static async Task<SyndicationFeed> ParseFeed(string feedUrl)
		{
			var httpClient = new HttpClient();
			var response = await httpClient.GetAsync(feedUrl);

			using (XmlReader reader = XmlReader.Create(await response.Content.ReadAsStreamAsync()))
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
