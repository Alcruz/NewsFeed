using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModusCreate.NewsFeed.Domain
{
	public class FeedEntry
	{
		public string Title { get; internal set; }
		public string Summary { get; internal set; }
		public DateTimeOffset PublishDate { get; internal set; }
		public DateTimeOffset LastUpdatedTime { get; internal set; }
		public string EntryId { get; internal set; }
	}
}
