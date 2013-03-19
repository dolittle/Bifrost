Bifrost.namespace("Bifrost.navigation", {
    NavigationFrame: Bifrost.Type.extend(function (uriMapper, home, history, viewFactory) {
        var self = this;

        this.uriMapper = uriMapper;
        this.home = home;
        this.container = null;

        this.currentView = ko.observable();

        history.Adapter.bind(window, "statechange", function () {
            self.handleView();
        });

        this.setContainer = function (container) {
            self.container = container;
            self.handleView();
        };

        this.handleView = function () {
            var viewPath = this.getCurrentViewPath();
            viewFactory.createFrom(viewPath).continueWith(function (view) {
                self.currentView(view);
                $(self.container).html(view.content);
            });
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