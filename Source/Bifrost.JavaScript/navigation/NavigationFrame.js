Bifrost.namespace("Bifrost.navigation", {
    NavigationFrame: Bifrost.Type.extend(function (home, uriMapper, history, viewManager) {
        var self = this;

        this.home = home;
        this.history = history;
        this.viewManager = viewManager;

        this.container = null;
        this.currentUri = ko.observable(home);
        this.uriMapper = uriMapper || null;

        this.setCurrentUri = function (path) {
            if (path.indexOf("/") == 0) path = path.substr(1);
            if (path.lastIndexOf("/") == path.length - 1) path = path.substr(0, path.length - 1);
            if (path == null || path.length == 0) path = self.home;
            if (self.uriMapper != null && !self.uriMapper.hasMappingFor(path)) path = self.home;
            self.currentUri(path);
        };

        this.setCurrentUriFromCurrentLocation = function () {
            var state = self.history.getState();

            /*
            if (state.url.indexOf("/?") > 0) {
                if (state.url.indexOf("/?") == state.url.length - 2) {
                    state.url = state.url.replace("/?", "");
                } else {
                    state.url = state.url.replace("/?", "?");
                }
                History.pushState(state.data, state.title, state.url);
            }*/

            var uri = Bifrost.Uri.create(state.url);
            self.setCurrentUri(uri.path);
        }

        history.Adapter.bind(window, "statechange", function () {
            self.setCurrentUriFromCurrentLocation();
        });
        
        this.configureFor = function (container) {
            self.setCurrentUriFromCurrentLocation();
            self.container = container;

            var uriMapper = $(container).closest("[data-urimapper]");
            if (uriMapper.length == 1) {
                var uriMapperName = $(uriMapper[0]).data("urimapper");
                if (uriMapperName in Bifrost.uriMappers) {
                    self.uriMapper = Bifrost.uriMappers[uriMapperName];
                }
            }
            if (self.uriMapper == null) self.uriMapper = Bifrost.uriMappers.default;
        };

        this.navigate = function (uri) {
            self.setCurrentUri(uri);
        };

    })
});