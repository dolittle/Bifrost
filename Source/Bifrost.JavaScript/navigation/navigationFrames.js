Bifrost.namespace("Bifrost.navigation", {
    navigationFrames: Bifrost.Singleton(function () {
        var self = this;

        this.hookup = function () {
            $("[data-navigation-frame]").each(function (index, element) {
                var configurationString = $(element).data("navigation-frame");
                var configurationItems = ko.expressionRewriting.parseObjectLiteral(configurationString);

                var configuration = {};

                for (var index = 0; index < configurationItems.length; index++) {
                    var item = configurationItems[index];
                    configuration[item.key.trim()] = item.value.trim();
                }

                if (typeof configuration.uriMapper !== "undefined") {
                    var mapper = Bifrost.uriMappers[configuration.uriMapper];
                    var frame = Bifrost.navigation.NavigationFrame.create({
                        stringMapper: mapper,
                        home: configuration.home || ''
                    });
                    frame.setContainer(element);

                    element.navigationFrame = frame;
                }
            });
        };
    })
});