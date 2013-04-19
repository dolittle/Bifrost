Bifrost.namespace("Bifrost.views", {
    viewFactory: Bifrost.Singleton(function () {
        var self = this;

        this.createFrom = function (path) {
            var promise = Bifrost.execution.Promise.create();

            var view = Bifrost.views.View.create();

            view.load(path).continueWith(function () {
                promise.signal(view);
            });

            return promise;
        };
    })
});