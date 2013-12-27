Bifrost.namespace("Bifrost", {
    systemEvents: Bifrost.Singleton(function () {
        this.readModels = Bifrost.read.readModelSystemEvents.create();
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.systemEvents = Bifrost.systemEvents;