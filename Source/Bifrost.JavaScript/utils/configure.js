Bifrost.namespace("Bifrost", {
    configure: (function () {
        var self = this;

        this.ready = false;
        this.readyCallbacks = [];

        function ready(callback) {
            if (self.ready == true) {
                callback();
            } else {
                readyCallbacks.push(callback);
            }
        }

        function onReady() {
            self.ready = true;
            for (var callbackIndex = 0; callbackIndex < self.readyCallbacks.length; callbackIndex++) {
                self.readyCallbacks[callbackIndex]();
            }
        }

        function onStartup() {
            var self = this;

            if (typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
                Bifrost.WellKnownTypesDependencyResolver.types.history = History;
            }

            var defaultUriMapper = Bifrost.navigation.UriMapper.create();
            defaultUriMapper.addMapping("{boundedContext}/{module}/{feature}", "/{boundedContext}/{module}/{feature}.html");
            defaultUriMapper.addMapping("{boundedContext}/{feature}", "/{boundedContext}/{feature}.html");
            defaultUriMapper.addMapping("{feature}", "/{feature}.html");
            Bifrost.navigation.uriMappers.default = defaultUriMapper;

            var promise = Bifrost.assetsManager.initialize();
            promise.continueWith(function () {
                self.onReady();
                Bifrost.navigation.navigationManager.hookup();
                Bifrost.features.featureManager.hookup($);
                Bifrost.navigation.navigationFrames.create().hookup();
            });
        }

        function reset() {
            self.ready = false;
            self.readyCallbacks = [];
        }

        return {
            ready: ready,
            onReady: onReady,
            onStartup: onStartup,
            reset: reset,
            isReady: function () {
                return self.ready;
            }
        }
    })()
});
(function ($) {
    $(function () {
        if( typeof Bifrost.assetsManager !== "undefined" ) {
            Bifrost.configure.onStartup();
        }
    });
})(jQuery);