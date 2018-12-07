using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModusCreate.NewsFeed.Domain;
using ModusCreate.NewsFeed.Service;
using ModusCreate.NewsFeed.Web.Models;

namespace ModusCreate.NewsFeed.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeedsController : ControllerBase
	{
		private readonly FeedService feedService;

		public FeedsController(FeedService feedService)
		{
			this.feedService = feedService;
		}

		[HttpGet("{subscriptionId}")]
		public async Task<IActionResult> Get(int subscriptionId)
		{
			var subscription = await this.feedService.GetSubscription(subscriptionId);
			if (subscription == null)
			{
				return NotFound();
			}

			var feed = await Feed.CreateFromUrl(subscription.FeedUrl);
			return Ok(FeedModel.Map(feed));
		}

		[HttpGet("{subscriptionId}/entries")]
		public async Task<IActionResult> GetEntries(int subscriptionId)
		{
			var subscription = await this.feedService.GetSubscription(subscriptionId);
			if (subscription == null)
			{
				return NotFound();
			}

			var feed = await Feed.CreateFromUrl(subscription.FeedUrl);
			return Ok(feed.Entries);
		}


		[HttpGet("subscriptions")]
		public async Task<IEnumerable<FeedSubscriptionModel>> GetSubscriptions()
		{
			return (await this.feedService.GetAll()).Select(FeedSubscriptionModel.Map);
		}

		[HttpGet("subscriptions/{subscriptionId:int}")]
		public async Task<IActionResult> GetSubscription(int subscriptionId)
		{
			var subscription = await this.feedService.GetSubscription(subscriptionId);
			if (subscription == null)
			{
				return NotFound();
			}

			return Ok(FeedSubscriptionModel.Map(subscription));
		}

		[HttpPost("subscribe")]
		public async Task<IActionResult> Subscribe([FromBody] SubscribeToFeed model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			try
			{
				var subscription = await this.feedService.Subscribe(model.FeedEntryUrl);
				return CreatedAtAction("GetSubscription", new { subscriptionId = subscription.Id });
			}
			catch (FeedException ex)
			{
				return BadRequest(new
				{
					Error = ex.Message
				});
			}
		}
	}
}