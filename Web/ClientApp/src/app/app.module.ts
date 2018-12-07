import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FeedService } from './common/feed.service';
import { FeedSubscriptionComponent } from './feed-subscription/feed-subscription.component';
import { FeedEntryListComponent } from './common/feed-entry-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FeedSubscriptionComponent,
    FeedEntryListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'subscription/:id', component: FeedSubscriptionComponent },
    ])
  ],
  providers: [FeedService],
  bootstrap: [AppComponent]
})
export class AppModule { }
