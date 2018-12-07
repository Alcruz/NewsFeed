import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { FeedSubscription } from "./feed-subscription.model";
import { Feed } from "./feed.model";
import { FeedEntry } from "./feed-entry.model";
import { BehaviorSubject, Subject } from "rxjs";

@Injectable()
export class FeedService {
  subscriptionsSubject: Subject<Array<FeedSubscription>>;
  entriesSubject: Subject<Array<FeedEntry>>;

  constructor(private httpClient: HttpClient) {
    this.subscriptionsSubject = new BehaviorSubject<Array<FeedSubscription>>([]);
    this.entriesSubject = new BehaviorSubject<Array<FeedEntry>>([]);
  } 

  public subscribe(feedEntryUrl: string) {
    return this.httpClient.post("api/feeds/subscribe", {
      feedEntryUrl
    });
  }

  public getSubscriptions() {
    this._getSubscriptions();
    return this.subscriptionsSubject.asObservable();
  }

  public getSubscription(id: number) {
    return this.httpClient.get<FeedSubscription>(`api/feeds/subscriptions/${id}`);
  }

  public get(id: number) {
    return this.httpClient.get<Feed>(`api/feeds/${id}`);
  }

  public getEntries(id: number) {
    return this.httpClient.get<Array<FeedEntry>>(`api/feeds/${id}/entries`);
  }

  public getAllEntries() {
    this._getAllEntries();
    return this.entriesSubject.asObservable();
  }

  public refrehsData() {
    this._getSubscriptions();
  }

  private _getSubscriptions() {
    this.httpClient.get<Array<FeedSubscription>>("api/feeds/subscriptions")
      .subscribe(subscriptions => this.subscriptionsSubject.next(subscriptions), error => this.subscriptionsSubject.error(error));
  }

  private _getAllEntries() {
    return this.httpClient.get<Array<FeedEntry>>(`api/feeds/entries`)
      .subscribe(entries => this.entriesSubject.next(entries), error => this.entriesSubject.error(error));
  }
}
