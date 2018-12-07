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

			return Ok(FeedModel.Map(subscription.Feed));
		}

		[HttpGet("entries")]
		public async Task<IActionResult> GetEntries()
		{
			return Ok(await this.feedService.GetAllFeedEntries());
		}

		[HttpGet("{subscriptionId}/entries")]
		public async Task<IActionResult> GetEntries(int subscriptionId, string searchTitle)
		{
			var subscription = await this.feedService.GetSubscription(subscriptionId);
			if (subscription == null)
			{
				return NotFound();
			}

			IEnumerable<FeedEntry> entries = subscription.Feed.Entries;
			if (!string.IsNullOrWhiteSpace(searchTitle))
			{
				entries = entries.Where(fe => fe.Title.Contains(searchTitle));
			}

			return Ok(entries);
		}


		[HttpGet("subscriptions")]
		public async Task<IEnumerable<FeedSubscriptionModel>> GetSubscriptions()
		{
			return (await this.feedService.GetAll()).OrderBy(s => s.Title).Select(FeedSubscriptionModel.Map);
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