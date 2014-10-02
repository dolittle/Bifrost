Bifrost.namespace("Bifrost.hubs", {
    hubConnection: Bifrost.Singleton(function () {
        var hub = $.hubConnection("/signalr", { useDefaultPath: false });

        this.createProxy = function (hubName) {
            var proxy = hub.createHubProxy(hubName);
            return proxy;
        };

    })
});
Bifrost.WellKnownTypesDependencyResolver.types.hubConnection = Bifrost.hubs.hubConnection;