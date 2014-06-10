Bifrost.namespace("Bifrost", {
    configureType: Bifrost.Singleton(function(assetsManager) {
        var self = this;

        var defaultUriMapper = Bifrost.StringMapper.create();
        defaultUriMapper.addMapping("{boundedContext}/{module}/{feature}/{view}", "{boundedContext}/{module}/{feature}/{view}.html");
        defaultUriMapper.addMapping("{boundedContext}/{feature}/{view}", "{boundedContext}/{feature}/{view}.html");
        defaultUriMapper.addMapping("{feature}/{view}", "{feature}/{view}.html");
        defaultUriMapper.addMapping("{view}", "{view}.html");
        Bifrost.uriMappers.default = defaultUriMapper;

        var bifrostVisualizerUriMapper = Bifrost.StringMapper.create();
        bifrostVisualizerUriMapper.addMapping("Visualizer/{module}/{view}", "/Bifrost/Visualizer/{module}/{view}.html");
        bifrostVisualizerUriMapper.addMapping("Visualizer/{view}", "/Bifrost/Visualizer/{view}.html");
        Bifrost.uriMappers.bifrostVisualizer = bifrostVisualizerUriMapper;

        this.isReady = false;
        this.readyCallbacks = [];

        this.initializeLandingPage = true;
        this.applyMasterViewModel = true;

        function onReady() {
            Bifrost.views.Region.current = document.body.region;
            self.isReady = true;
            for (var callbackIndex = 0; callbackIndex < self.readyCallbacks.length; callbackIndex++) {
                self.readyCallbacks[callbackIndex]();
            }
        }

        function hookUpNavigaionAndApplyViewModel() {
            Bifrost.navigation.navigationManager.hookup();

            if (self.applyMasterViewModel === true) {
                Bifrost.views.viewModelManager.create().masterViewModel.apply();
            }
        }

        function onStartup() {
            var configurators = Bifrost.configurator.getExtenders();
            configurators.forEach(function (configuratorType) {
                var configurator = configuratorType.create()
                configurator.config(self);
            });


            Bifrost.dependencyResolvers.DOMRootDependencyResolver.documentIsReady();
            Bifrost.views.viewModelBindingHandler.initialize();
            Bifrost.views.viewBindingHandler.initialize();
            Bifrost.navigation.navigationBindingHandler.initialize();

            if (typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
                Bifrost.WellKnownTypesDependencyResolver.types.history = History;
            }

            assetsManager.initialize().continueWith(function () {
                if (self.initializeLandingPage === true) {
                    Bifrost.views.viewManager.create().initializeLandingPage().continueWith(hookUpNavigaionAndApplyViewModel);
                } else {
                    hookUpNavigaionAndApplyViewModel();
                }
                onReady();
            });
        }

        function reset() {
            self.isReady = false;
            self.readyCallbacks = [];
        }

        this.ready = function(callback) {
            if (self.isReady == true) {
                callback();
            } else {
                self.readyCallbacks.push(callback);
            }
        };

        $(function () {
            onStartup();
        });
    })
});
Bifrost.configure = Bifrost.configureType.create();
