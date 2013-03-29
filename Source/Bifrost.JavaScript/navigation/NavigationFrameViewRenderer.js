Bifrost.namespace("Bifrost.navigation", {
    NavigationFrameViewRenderer: Bifrost.views.ViewRenderer.extend(function () {

        this.canRender = function (element) {
            return typeof $(element).data("navigation-frame") !== "undefined" && 
                    typeof $(element).data("view") === "undefined";
        };

        this.render = function (element) {
            var promise = Bifrost.execution.Promise.create();

            var configurationString = $(element).data("navigation-frame");
            var configurationItems = ko.expressionRewriting.parseObjectLiteral(configurationString);

            var configuration = {};

            for (var index = 0; index < configurationItems.length; index++) {
                var item = configurationItems[index];
                configuration[item.key.trim()] = item.value.trim();
            }

            if (typeof configuration.uriMapper !== "undefined") {
                $(element).data("urimapper", configuration.uriMapper);
            } else {
                configuration.uriMapper = "default";
            }

            var frame = Bifrost.navigation.NavigationFrame.create({
                home: configuration.home || '',
                locationAware: configuration.locationAware || true,
                uriMapper: Bifrost.uriMappers[configuration.uriMapper]
            });
            element.navigationFrame = frame;
            frame.setContainer(element).continueWith(function (view) {
                promise.signal(view);
            });

            return promise;
        };
    })
});
if (typeof Bifrost.views.viewRenderers != "undefined") {
    Bifrost.views.viewRenderers.NavigationFrameViewRenderer = Bifrost.navigation.NavigationFrameViewRenderer;
}
