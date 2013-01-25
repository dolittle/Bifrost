Bifrost.namespace("web.features.eventSubscriptions", {
    index: Bifrost.Type.extend(function(replayAll) {
        var self = this;
        this.subscriptions = ko.observableArray();

        this.replayAll = replayAll; 

        this.loadSubscriptions = function () {
            self.subscriptions([]);
            $.get("/EventSubscriptions/GetAll", {}, function (result) {
                self.subscriptions(result);
            }, "json");
        };

        this.refresh = function () {
            self.loadSubscriptions();
        };

        this.loadSubscriptions();
    })
});

Bifrost.features.featureManager.get("EventSubscriptions/index").defineViewModel(web.features.eventSubscriptions.index);
