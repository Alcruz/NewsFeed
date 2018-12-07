import { Component, OnInit } from '@angular/core';
import { FeedService } from '../common/feed.service';
import { FeedSubscription } from '../common/feed-subscription.model';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  subscriptions: Array<FeedSubscription> = [];
  isExpanded = false;

  constructor(private feedService: FeedService) {
  }

  ngOnInit(): void {
    this.getAllEntries().subscribe(subscriptions => this.subscriptions = subscriptions);
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  getAllEntries() {
    return this.feedService.getSubscriptions();
  }
}
