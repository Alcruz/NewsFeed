import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class FeedService {
  constructor(private httpClient: HttpClient) {
  } 

  subscribe(feedEntryUrl: string) {
    return this.httpClient.post("api/feeds/subscribe", {
      feedEntryUrl
    });
  }
}
