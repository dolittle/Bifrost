Bifrost.namespace("Bifrost.views", {
    ViewBindingHandlerTemplateSource: Bifrost.Type.extend(function (UIManager, viewModelManager, viewModelTypes, documentService, pathResolvers, viewFactory, regionManager, element, viewUri, allBindingsAccessor) {
        var self = this;
        var loaded = false;
        var nullView = viewFactory.createFrom("");
        nullView.content = "<notLoaded></notLoaded>";
        var view = ko.observable(nullView);
        var retainViewModel = allBindingsAccessor().retainViewModel || false;
        var isBusyObservable = allBindingsAccessor().isBusyObservable || ko.observable(false);

        var viewPath;

        var previousViewModel = null;

        this.currentElement = element;

        function load() {
            loaded = true;
            var uri = ko.utils.unwrapObservable(viewUri);
            viewPath = uri;

            var viewModelParameters = allBindingsAccessor().viewModelParameters || null;
            
            var actualPath = pathResolvers.resolve(element, uri);
            var actualView = viewFactory.createFrom(actualPath);
            actualView.element = element;

            isBusyObservable(true);

            if (documentService.hasOwnRegion(element)) {
                regionManager.evict(element.region);
                documentService.clearRegionOn(element);
            }

            regionManager.getFor(actualView).continueWith(function (region) {

                actualView.load(region).continueWith(function (loadedView) {
                    viewModelTypes.beginCreateInstanceOfViewModel(loadedView.viewModelPath, region, viewModelParameters).continueWith(function (viewModel) {
                        if (!Bifrost.isNullOrUndefined(viewModel)) {
                            region.viewModel = viewModel;
                        }

                        if (retainViewModel === true) {
                            previousViewModel = viewModel;
                        }
                        self.currentElement.loadedViewModel = viewModel;

                        var wrapper = document.createElement("div");
                        wrapper.innerHTML = loadedView.content;
                        UIManager.handle(wrapper);

                        loadedView.content = wrapper.innerHTML;

                        view(loadedView);

                        isBusyObservable(false);
                    });
                });
            });
        }

        function clear() {
            viewModelManager.masterViewModel.clearFor(element);
            documentService.cleanChildrenOf(element);
            view(nullView);
        }

        this.data = function (key, value) { };

        this.createAndSetViewModelFor = function (bindingContext, viewModelParameters) {
            if (!Bifrost.isNullOrUndefined(self.currentElement.loadedViewModel)) {
                bindingContext.$data = self.currentElement.loadedViewModel;
                self.currentElement.loadedViewModel = null;
                return;
            }

            if (!Bifrost.isNullOrUndefined(self.currentElement.currentViewModel)) {
                bindingContext.$data = self.currentElement.currentViewModel;
                return;
            }


            if (retainViewModel === true && !Bifrost.isNullOrUndefined(previousViewModel)) {
                bindingContext.$data = previousViewModel;
                return;
            }

            if (!Bifrost.isNullOrUndefined(view()) && !Bifrost.isNullOrUndefined(view().viewModelType)) {
                var region = view().region;
                viewModelParameters.region = region;

                var lastRegion = Bifrost.views.Region.current;
                Bifrost.views.Region.current = region;

                var viewModel = view().viewModelType.create(viewModelParameters);
                bindingContext.$data = viewModel;
                Bifrost.views.Region.current = lastRegion;
                self.currentElement.currentViewModel = viewModel;

                if (retainViewModel === true) {
                    previousViewModel = viewModel;
                }
            }
        };

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