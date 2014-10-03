Bifrost.namespace("Web.HumanResources.eventSubscriptions", {
    index: Bifrost.Type.extend(function(replayAll, allEventSubscriptions) {
        var self = this;
        this.subscriptions = allEventSubscriptions.all();
        this.replayAll = replayAll; 
    })
});
Bifrost.features.featureManager.get("EventSubscriptions/index").defineViewModel(Web.HumanResources.eventSubscriptions.index);
