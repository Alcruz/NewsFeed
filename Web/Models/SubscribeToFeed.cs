using System.ComponentModel.DataAnnotations;

namespace ModusCreate.NewsFeed.Web.Models
{
	public class SubscribeToFeed
    {
		[Required]
		[Url]
		public string FeedEntryUrl { get; set; }
	}
}
