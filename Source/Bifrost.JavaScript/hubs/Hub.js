Bifrost.namespace("Bifrost.hubs", {
    Hub: Bifrost.Type.extend(function (hubConnection) {
        var self = this;

        var proxy = null;
        this._name = "";

        function makeClientProxyFunction(callback, args) {
            return function () {
                callback.apply(self, args);
            };
        }

        this.client = function (client) {
            for (var property in client) {
                var value = client[property];
                if (!Bifrost.isFunction(value)) {
                    continue;
                }

                proxy.on(property, makeClientProxyFunction(value, arguments));
            }
        };

        this.server = {};

        this.invokeServerMethod = function (method, args) {
            var promise = Bifrost.execution.Promise.create();
            proxy.invoke(method, args).done(function (result) {
                promise.signal(result);
            });
            return promise;
        };

        this.onCreated = function (lastDescendant) {
            proxy = hubConnection.createProxy(lastDescendant._name);
        };
    })
});
