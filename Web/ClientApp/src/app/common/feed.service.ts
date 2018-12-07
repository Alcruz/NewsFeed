import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { FeedSubscription } from "./feed-subscription.model";

@Injectable()
export class FeedService {
  constructor(private httpClient: HttpClient) {
  } 

  public subscribe(feedEntryUrl: string) {
    return this.httpClient.post("api/feeds/subscribe", {
      feedEntryUrl
    });
  }

  public getSubscriptions() {
    return this.httpClient.get<Array<FeedSubscription>>("api/feeds/subscriptions");
  }
}
