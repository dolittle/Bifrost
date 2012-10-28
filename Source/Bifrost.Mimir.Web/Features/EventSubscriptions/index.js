Bifrost.features.featureManager.get("EventSubscriptions/index").defineViewModel(function () {
    var self = this;
    this.subscriptions = ko.observableArray();

    $.get("/EventSubscriptions/GetAll", {}, function (result) {
        $.each(result, function (index, item) {
            item.replayAll = Bifrost.commands.Command.create({
                name: "ReplayAllForEventSubscription"
            });
        });
        self.subscriptions(result);
    }, "json");


});