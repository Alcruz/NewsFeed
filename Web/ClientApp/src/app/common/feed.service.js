"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var FeedService = /** @class */ (function () {
    function FeedService(httpClient) {
        this.httpClient = httpClient;
    }
    FeedService.prototype.subscribe = function (feedEntryUrl) {
        return this.httpClient.post("api/feeds/subscribe", {
            feedEntryUrl: feedEntryUrl
        });
    };
    return FeedService;
}());
exports.FeedService = FeedService;
//# sourceMappingURL=feed.service.js.map