﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModusCreate.NewsFeed.Domain
{
    public class FeedSubscription
    {
		public int Id { get; set; }
		public string Title { get; set; }
		public string FeedUrl { get; set; }
		public Feed Feed { get; set; }
	}
}
