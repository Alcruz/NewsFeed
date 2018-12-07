using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModusCreate.NewsFeed.Domain;
using ModusCreate.NewsFeed.Service;
using ModusCreate.NewsFeed.Web.Models;

namespace Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeedController : ControllerBase
	{
		private readonly FeedService feedService;

		public FeedController(FeedService feedService)
		{
			this.feedService = feedService;
		}

		[HttpGet("subscriptions/{subscriptionId}")]
		public Task GetSubscription(string subscriptionId)
		{
			return Task.CompletedTask;
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