import { Component, Input } from "@angular/core";
import { FeedEntry } from "./feed-entry.model";

@Component({
  selector: 'app-feed-entry-list',
  templateUrl: './feed-entry-list.component.html',
  styleUrls: ['./feed-entry-list.component.css']
})
export class FeedEntryListComponent {
  @Input() entries: Array<FeedEntry> = [];
}
