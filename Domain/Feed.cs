using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		public string Description => this.feed.Description.Text;
		public Uri ImageUrl => this.feed.ImageUrl;

		public IEnumerable<FeedEntry> Entries => GetEntries();
		
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

		public static async Task<Feed> CreateFromUrl(string feedUrl) => new Feed(feedUrl, await ParseFeed(feedUrl));

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
				Title = Title,
				FeedUrl = FeedUrl
			};
		}

		private IEnumerable<FeedEntry> GetEntries()
		{
			return this.feed.Items.Select(item => new FeedEntry
			{
				EntryId = item.Id,
				Title = item.Title.Text,
				Summary = item.Summary.Text,
				PublishDate = item.PublishDate,
				LastUpdatedTime = item.LastUpdatedTime
			});
		}
	}

	public class FeedException : Exception
	{
		public FeedException(string message) : base(message)
		{
		}
	}
}
