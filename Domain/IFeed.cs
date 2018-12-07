using System;
using System.Collections.Generic;

namespace ModusCreate.NewsFeed.Domain
{
	public interface IFeed
	{
		string Title { get; }
		string Description { get; }
		string FeedUrl { get; }
		string ImageUrl { get; }

		List<FeedEntry> Entries { get; }
	}
}