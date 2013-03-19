Bifrost.namespace("Bifrost.views", {
    viewLoader: Bifrost.Singleton(function () {
        var self = this;
        
        this.load = function (path) {
            var promise = Bifrost.execution.Promise.create();
            require(["text!" + path + "!strip"], function (view) {
                promise.signal(view);
            });
            return promise;
        };
    })
});