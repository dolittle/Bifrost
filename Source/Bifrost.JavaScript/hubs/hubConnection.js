Bifrost.namespace("Bifrost.hubs", {
    hubConnection: Bifrost.Singleton(function () {
        var hub = $.hubConnection("/signalr", { useDefaultPath: false });

        this.createProxy = function (hubName) {
            var proxy = hub.createHubProxy(hubName);
            return proxy;
        };

        $.connection.hub.start().done(function () {
            console.log("Hub connection up and running");
        });
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.hubConnection = Bifrost.hubs.hubConnection;