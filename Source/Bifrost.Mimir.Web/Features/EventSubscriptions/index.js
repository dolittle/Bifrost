Bifrost.features.featureManager.get("EventSubscriptions/index").defineViewModel(function () {
    var self = this;
    this.subscriptions = ko.observableArray();

    this.replayAll = Bifrost.commands.Command.create({
        name: "ReplayAll",
        complete: function () {
            setTimeout(function () {
                self.loadSubscriptions();
            }, 500);
        }
    });


    this.loadSubscriptions = function () {
        self.subscriptions([]);
        $.get("/EventSubscriptions/GetAll", {}, function (result) {
            $.each(result, function (index, item) {
                item.replayAllForSubscription = Bifrost.commands.Command.create({
                    name: "ReplayAllForEventSubscription",
                    parameters: {
                        eventSubscriptionId: ko.observable(item.id)
                    },
                    complete: function () {
                        setTimeout(function () {
                            self.loadSubscriptions();
                        }, 500);
                    }
                });
            });
            self.subscriptions(result);
        }, "json");
    };

    this.refresh = function () {
        self.loadSubscriptions();
    };

    this.loadSubscriptions();
});