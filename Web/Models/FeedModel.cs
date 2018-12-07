using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModusCreate.NewsFeed.Domain;

namespace ModusCreate.NewsFeed.Web.Models
{
	public class FeedModel
	{
		public string FeedUrl { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }

		internal static FeedModel Map(IFeed feed)
		{
			return new FeedModel
			{
				FeedUrl = feed.FeedUrl,
				Title = feed.Title,
				Description = feed.Description,
				ImageUrl = feed.ImageUrl,
			};
		}
	}
}
