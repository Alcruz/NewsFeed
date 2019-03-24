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

  changeIcon($event) {
    if ($event.type === 'mouseover') {
      $event.currentTarget.children[0].classList.remove('glyphicon-list-alt');
      $event.currentTarget.children[0].classList.add('glyphicon-remove');
    } else {
      $event.currentTarget.children[0].classList.remove('glyphicon-remove');
      $event.currentTarget.children[0].classList.add('glyphicon-list-alt');
    }
  }

  remove(id, event) {
    let confirmation = confirm("Are you sure you want to delete this?");
    if (confirmation) {
      this.feedService.remove(id).subscribe(f => console.log(f));
      this.getAllEntries().subscribe(subscriptions => this.subscriptions = subscriptions);
    }
  }
}
