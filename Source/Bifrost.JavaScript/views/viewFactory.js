Bifrost.namespace("Bifrost.views", {
    viewFactory: Bifrost.Singleton(function () {
        var self = this;

        this.createFrom = function (path) {
            var view = Bifrost.views.View.create({
                path: path
            });
            return view;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.viewFactory = Bifrost.views.viewFactory;