import { Component, OnInit } from '@angular/core';
import { FeedService } from '../common/feed.service';
import { ActivatedRoute } from '@angular/router';
import { Feed } from '../common/feed.model';
import { FeedEntry } from '../common/feed-entry.model';

@Component({
  selector: 'app-feed-subscription',
  templateUrl: './feed-subscription.component.html',
  styleUrls: ['./feed-subscription.component.css']
})
export class FeedSubscriptionComponent implements OnInit {
  id: number;
  feed: Feed;
  entries: Array<FeedEntry> = [];

  constructor(private feedService: FeedService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.id = parseInt(this.route.snapshot.paramMap.get('id') || '');
    this.feedService.get(this.id)
      .subscribe(feed => this.feed = feed);

    this.feedService.getEntries(this.id)
      .subscribe(entries => this.entries = entries);
  }
}
