Bifrost.namespace("Bifrost.views", {
    ViewBindingHandlerTemplateSource: Bifrost.Type.extend(function (UIManager, viewModelManager, viewModelTypes, documentService, pathResolvers, viewFactory, regionManager, element, viewUri, allBindingsAccessor) {
        var self = this;
        var loaded = false;
        var nullView = viewFactory.createFrom("");
        nullView.content = "<notLoaded></notLoaded>";
        var view = ko.observable(nullView);
        
        function isLoaded() {
            var loadedView = view();
            return !Bifrost.isNullOrUndefined(loadedView) && loadedView !== nullView;
        };

        var isBusyObservable = allBindingsAccessor().isBusyObservable || ko.observable(false);

        var viewPath;

        var previousViewModel = null;

        function load() {
            loaded = true;
            var uri = ko.utils.unwrapObservable(viewUri);
            viewPath = uri;

            var viewModelParameters = allBindingsAccessor().viewModelParameters || null;
            
            var actualPath = pathResolvers.resolve(element, uri);
            var actualView = viewFactory.createFrom(actualPath);
            actualView.element = element;

            isBusyObservable(true);
            
            regionManager.getFor(actualView).continueWith(function (region) {

                actualView.load(region).continueWith(function (loadedView) {
                    var wrapper = document.createElement("div");
                    wrapper.innerHTML = loadedView.content;
                    UIManager.handle(wrapper);
                    loadedView.content = wrapper.innerHTML;

                    var viewModelType = loadedView.viewModelType;
                    if (!Bifrost.isNullOrUndefined(viewModelType)) {
                        var lastRegion = Bifrost.views.Region.current;
                        Bifrost.views.Region.current = region;

                        viewModelType.ensure().continueWith(function () {
                            Bifrost.views.Region.current = lastRegion;
                            self.viewLoaded.trigger(loadedView);
                            view(loadedView);
                            isBusyObservable(false);
                        });
                    } else {
                        self.viewLoaded.trigger(loadedView);
                        view(loadedView);
                        isBusyObservable(false);
                    }
                });
            });
        }

        function clear() {
            viewModelManager.masterViewModel.clearFor(element);
            documentService.cleanChildrenOf(element);
            view(nullView);
        }

        this.viewLoaded = Bifrost.Event.create();

        this.data = function (key, value) { };

        this.text = function (value) {
            var uri = ko.utils.unwrapObservable(viewUri);
            if (Bifrost.isNullOrUndefined(uri) || uri === "") {
                clear();
                loaded = false;
            } else {
                if (!loaded) {
                    load();
                } 
            }
            return view().content;
        };
    })
});