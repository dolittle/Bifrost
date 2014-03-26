Bifrost.namespace("Bifrost", {
    systemEvents: Bifrost.Singleton(function () {
        this.readModels = Bifrost.read.readModelSystemEvents.create();
        this.commands = Bifrost.commands.commandEvents.create();
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.systemEvents = Bifrost.systemEvents;