Bifrost.namespace("Bifrost.navigation", {
    NavigationFrame: Bifrost.Type.extend(function (uriMapper, home, history) {
        var self = this;

        this.uriMapper = uriMapper;
        this.home = home;
        this.container = null;

        history.Adapter.bind(window, "statechange", function () {
        });

        this.setContainer = function (container) {
            self.container = container;

            var viewPath = this.getCurrentViewPath();
        };

        this.getCurrentViewPath = function () {
            var state = history.getState();
            var uri = Bifrost.Uri.create(state.url);

            var path = uri.path;
            if (path.indexOf("/") == 0) path = path.substr(1);

            var viewPath = uriMapper.resolve(path);
            if (viewPath == "") viewPath = uriMapper.resolve(self.home);

            return viewPath;
        };
    })
});