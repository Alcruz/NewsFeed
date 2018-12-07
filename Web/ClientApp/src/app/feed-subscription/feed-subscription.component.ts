import { Component, OnInit } from '@angular/core';
import { FeedService } from '../common/feed.service';
import { ActivatedRoute } from '@angular/router';
import { FeedSubscription } from '../common/feed-subscription.model';

@Component({
  selector: 'app-feed-subscription',
  templateUrl: './feed-subscription.component.html',
  styleUrls: ['./feed-subscription.component.css']
})
export class FeedSubscriptionComponent implements OnInit {
  subscription: FeedSubscription;

  constructor(private feedService: FeedService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    this.feedService.getSubscription(parseInt(id))
      .subscribe(subscription => this.subscription = subscription);
  }
}
