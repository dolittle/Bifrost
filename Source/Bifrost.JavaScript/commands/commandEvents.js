Bifrost.namespace("Bifrost.commands", {
    commandEvents: Bifrost.Singleton(function () {
        this.succeeded = Bifrost.Event.create();
        this.failed = Bifrost.Event.create();
    })
});