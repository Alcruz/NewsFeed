using System;
using System.Collections.Generic;

namespace ModusCreate.NewsFeed.Domain
{
	public class Feed : IFeed
	{
		public Feed()
		{
			Entries = new List<FeedEntry>();
		}

		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string FeedUrl { get; set; }
		public string ImageUrl { get; set; }
		public List<FeedEntry> Entries { get; set; }
	}
}
