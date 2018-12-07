import { Component, OnInit } from '@angular/core';
import { FeedService } from '../common/feed.service';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Feed } from '../common/feed.model';
import { FeedEntry } from '../common/feed-entry.model';
import { switchMap } from 'rxjs/operators/switchMap'


@Component({
  selector: 'app-feed-subscription',
  templateUrl: './feed-subscription.component.html',
  styleUrls: ['./feed-subscription.component.css']
})
export class FeedSubscriptionComponent implements OnInit {
  id: number;
  feed: Feed;
  searchTitle: string;
  entries: Array<FeedEntry> = [];

  constructor(private feedService: FeedService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.feedService.get(parseInt(params.get('id'))))
    ).subscribe(feed => this.feed = feed);


    this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.feedService.getEntries(parseInt(params.get('id'))))
    ).subscribe(entries => this.entries = entries);
  }

  search() {
    const id = this.route.snapshot.paramMap.get('id');
    this.feedService.getEntries(parseInt(id), this.searchTitle)
      .subscribe(entries => this.entries = entries);
  }
}
