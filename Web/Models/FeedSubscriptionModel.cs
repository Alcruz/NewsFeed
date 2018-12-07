using ModusCreate.NewsFeed.Domain;

namespace ModusCreate.NewsFeed.Web.Models
{
	public class FeedSubscriptionModel
	{
		public int Id { get; set; }
		public string Title { get; set; }

		public static FeedSubscriptionModel Map(FeedSubscription feedSubscription) {
			return new FeedSubscriptionModel
			{
				Id = feedSubscription.Id,
				Title = feedSubscription.Title
			};
		}
	}
}