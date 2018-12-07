import { Component } from '@angular/core';
import { FeedService } from './feed.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  feedEntry: string = '';

  constructor(private feedService: FeedService) {
  }

  addFeedEntry() {
    this.feedService.subscribe(this.feedEntry);
  }
}
