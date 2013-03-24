Bifrost.namespace("Bifrost.navigation", {
    NavigationFrame: Bifrost.Type.extend(function (home, history, viewManager) {
        var self = this;

        this.viewManager = viewManager;
        this.home = home;
        this.container = null;
        this.currentUri = ko.observable(home);
        this.uriMapper = null;

        this.currentUri.subscribe(function () {
            self.render();
        });

        history.Adapter.bind(window, "statechange", function () {
            var state = history.getState();
            var uri = Bifrost.Uri.create(state.url);
            var path = uri.path;
            if (path.indexOf("/") == 0) path = path.substr(1);
            if (!self.uriMapper.hasMappingFor(path)) path = self.home;
            self.currentUri(path);
        });

        this.setContainer = function (container) {
            self.container = container;

            var uriMapper = $(container).closest("[data-urimapper]");
            if (uriMapper.length == 1) {
                var uriMapperName = $(uriMapper[0]).data("urimapper");
                if (uriMapperName in Bifrost.uriMappers) {
                    self.uriMapper = Bifrost.uriMappers[uriMapperName];
                }
            }
            if (self.uriMapper == null) self.uriMapper = Bifrost.uriMappers.default;

            return self.render();
        };

        this.render = function () {
            var promise = Bifrost.execution.Promise.create();
            var path = self.currentUri();
            if (path !== null && typeof path !== "undefined") {
                $(self.container).data("view", path);
                self.viewManager.render(self.container).continueWith(function (view) {
                    promise.signal(view);
                });
            }
            return promise;
        };

        this.navigate = function (uri) {
            self.currentUri(uri);
        };
    })
});