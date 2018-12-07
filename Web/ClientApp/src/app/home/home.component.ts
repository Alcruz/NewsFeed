import { Component, OnInit } from '@angular/core';
import { FeedService } from '../common/feed.service';
import { FeedEntry } from '../common/feed-entry.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  entries: Array<FeedEntry> = [];
  feedEntry: string = '';

  constructor(private feedService: FeedService, private toastr: ToastrService) {
  }

  ngOnInit() {
    this.feedService.getAllEntries()
      .subscribe(entries => this.entries = entries);
  }

  subscribe() {
    this.feedService.subscribe(this.feedEntry)
      .subscribe(
      _ => this.toastr.success('News Feed Added Successfully', 'Add News Feed'),
      errResponse => this.toastr.error('Error Adding News Fead. Reason: ' + errResponse.error.error, 'Add News Feed')
      );
  }
}
