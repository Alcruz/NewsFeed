import { Component, OnInit } from '@angular/core';
import { FeedService } from '../common/feed.service';
import { FeedEntry } from '../common/feed-entry.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  entries: Array<FeedEntry> = [];
  feedEntry: string = '';

  constructor(private feedService: FeedService) {
  }

  ngOnInit() {
    this.feedService.getAllEntries()
      .subscribe(entries => this.entries = entries);
  }

  subscribe() {
    this.feedService.subscribe(this.feedEntry)
      .subscribe(_ => console.log("adding new feed"));

  }
}
