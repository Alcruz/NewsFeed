using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace ModusCreate.NewsFeed.Domain
{
	public class RssFeed : IFeed
	{
		private SyndicationFeed feed;

		private RssFeed(string feedUrl, SyndicationFeed feed)
		{
			FeedUrl = feedUrl;
			this.feed = feed;
		}

		public string FeedUrl { get; }
		public string Title => this.feed.Title?.Text ?? string.Empty;
		public string Description => this.feed.Description?.Text ?? string.Empty;
		public string ImageUrl => this.feed.ImageUrl?.ToString() ?? string.Empty;

		public List<FeedEntry> Entries => GetEntries().ToList();


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

		public static async Task<RssFeed> CreateFromUrl(string feedUrl) => new RssFeed(feedUrl, await ParseFeed(feedUrl));

		private static async Task<SyndicationFeed> ParseFeed(string feedUrl)
		{
			HttpResponseMessage response = await DownloadFeed(feedUrl);

			using (XmlReader reader = XmlReader.Create(await response.Content.ReadAsStreamAsync()))
			{
				return SyndicationFeed.Load(reader);
			}
		}

		private static async Task<HttpResponseMessage> DownloadFeed(string feedUrl)
		{
			var httpClient = new HttpClient();
			var response = await httpClient.GetAsync(feedUrl);
			return response;
		}

		public FeedSubscription SubscribeTo()
		{
			return new FeedSubscription
			{
				Id = 0,
				Title = Title,
				FeedUrl = FeedUrl,
				Feed = new Feed
				{
					Id = 0,
					Title = Title,
					Description = Description,
					ImageUrl = ImageUrl,
					FeedUrl = FeedUrl,
					Entries = Entries
				}
			};
		}

		private IEnumerable<FeedEntry> GetEntries()
		{
			return this.feed.Items.Select(item => new FeedEntry
			{
				Id = item.Id,
				FeedId = 0,
				Title = item.Title?.Text,
				Summary = item.Summary?.Text,
				PublishDate = item.PublishDate,
				LastUpdatedTime = item.LastUpdatedTime
			}).Distinct();
		}
	}

	public class FeedException : Exception
	{
		public FeedException(string message) : base(message)
		{
		}
	}
}
