import { Component } from '@angular/core';
import { FeedService } from '../common/feed.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  feedEntry: string = '';

  constructor(private feedService: FeedService) {
  }

  subscribe() {
    this.feedService.subscribe(this.feedEntry)
      .subscribe(_ => console.log("adding new feed"));
  }
}
