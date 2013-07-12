Bifrost.namespace("Bifrost.views", {
    viewModelLoader: Bifrost.Singleton(function () {
        var self = this;

        this.load = function (path) {
            var promise = Bifrost.execution.Promise.create();
            if (!path.startsWith("/")) path = "/" + path;
            require([path], function () {
                promise.signal(instance);
            });
            return promise;
        };
    })
});
