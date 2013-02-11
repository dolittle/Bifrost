Bifrost.namespace("web.features.eventSubscriptions", {
    index: Bifrost.Type.extend(function(replayAll, allEventSubscriptions) {
        var self = this;
        this.subscriptions = allEventSubscriptions.all();
        this.replayAll = replayAll; 
    })
});
Bifrost.features.featureManager.get("EventSubscriptions/index").defineViewModel(web.features.eventSubscriptions.index);
