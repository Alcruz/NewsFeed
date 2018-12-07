using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModusCreate.NewsFeed.Domain
{
	public class FeedEntry
	{
		public string Id { get; internal set; }
		public string Title { get; internal set; }
		public string Summary { get; internal set; }
		public DateTimeOffset PublishDate { get; internal set; }
		public DateTimeOffset LastUpdatedTime { get; internal set; }
		public int FeedId { get; set; }


		public override bool Equals(object obj)
		{
			FeedEntry src = obj as FeedEntry;
			if (src == null) return false;

			return src.Id.Equals(Id);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}
