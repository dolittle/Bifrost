Bifrost.namespace("Bifrost.views", {
    ViewBindingHandlerTemplateSource: Bifrost.Type.extend(function (UIManager, viewModelManager, viewModelTypes, documentService, pathResolvers, viewFactory, regionManager, element, viewUri, allBindingsAccessor) {
        var self = this;
        var loaded = false;
        var nullView = viewFactory.createFrom("");
        nullView.content = "<notLoaded></notLoaded>";
        var view = ko.observable(nullView);
        var isLoaded = ko.computed(function () {
            var loadedView = view();
            return !Bifrost.isNullOrUndefined(loadedView) && loadedView !== nullView;
        });
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

            
            regionManager.getFor(actualView).continueWith(function (region) {

                actualView.load(region).continueWith(function (loadedView) {

                    // Make sure all dependencies for viewModel type is loaded! (Type.ensure())

                    // When ensured (continueWith) - view is loaded ( view(loadedView) ) - 
                    // add ko.computed() called isLoaded which checks view observable for null or undefined
                    var wrapper = document.createElement("div");
                    wrapper.innerHTML = loadedView.content;
                    UIManager.handle(wrapper);
                    loadedView.content = wrapper.innerHTML;

                    var viewModelType = loadedView.viewModelType;
                    if (!Bifrost.isNullOrUndefined(viewModelType)) {
                        viewModelType.ensure().continueWith(function () {
                            console.log("Ensured viewmodel for: " + loadedView.path);
                            view(loadedView);
                        });
                    } else {
                        console.log("Ensured view for: " + loadedView.path);
                        view(loadedView);
                    }


                    //viewModelTypes.beginCreateInstanceOfViewModel(loadedView.viewModelPath, region, viewModelParameters).continueWith(function (viewModel) {
                    //    if (!Bifrost.isNullOrUndefined(viewModel)) {
                    //        region.viewModel = viewModel;
                    //    }

                    //    if (retainViewModel === true) {
                    //        previousViewModel = viewModel;
                    //    }
                    //    self.currentElement.loadedViewModel = viewModel;

                    //    var wrapper = document.createElement("div");
                    //    wrapper.innerHTML = loadedView.content;
                    //    UIManager.handle(wrapper);

                    //    loadedView.content = wrapper.innerHTML;

                    //    view(loadedView);

                    //    isBusyObservable(false);
                    //});
                });
            });
        }

        function clear() {
            viewModelManager.masterViewModel.clearFor(element);
            documentService.cleanChildrenOf(element);
            self.currentElement.currentViewModel = undefined;
            view(nullView);
        }

        this.data = function (key, value) { };

        this.createAndSetViewModelFor = function (bindingContext, viewModelParameters) {
            // if !isLoaded() - return

            // If isLoaded() - region stuff (see before) - create instance of ViewModel - unless it is being retained and an instance exists!


            /*
            if (!Bifrost.isNullOrUndefined(self.currentElement.loadedViewModel)) {
                bindingContext.$data = self.currentElement.loadedViewModel;
                self.currentElement.loadedViewModel = null;
                return;
            }

            if (!Bifrost.isNullOrUndefined(self.currentElement.currentViewModel)) {
                bindingContext.$data = self.currentElement.currentViewModel;
                return;
            }
            */

            if (isLoaded() === false) return;

            // All region stuff should be in createAndSetViewModel... 
            if (documentService.hasOwnRegion(element)) {
                regionManager.evict(element.region);
                documentService.clearRegionOn(element);
            }

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

            
            var region = view().region;
            
            viewModelParameters.region = region;

            var lastRegion = Bifrost.views.Region.current;
            Bifrost.views.Region.current = region;

            var viewModel = null;
            var viewModelType = view().viewModelType;
            if (!Bifrost.isNullOrUndefined(viewModelType)) {
                viewModel = view().viewModelType.create(viewModelParameters);
            }
            var loadedView = view();

            if (!Bifrost.isNullOrUndefined(viewModel)) {
                region.viewModel = viewModel;
            }

            if (retainViewModel === true) {
                previousViewModel = viewModel;
            }

            self.currentElement.loadedViewModel = viewModel;

            bindingContext.$data = viewModel;
            Bifrost.views.Region.current = lastRegion;
            self.currentElement.currentViewModel = viewModel;

            if (retainViewModel === true) {
                previousViewModel = viewModel;
            }
            isBusyObservable(false);

        };

        this.text = function (value) {
            var uri = ko.utils.unwrapObservable(viewUri);
            if (Bifrost.isNullOrUndefined(uri) || uri === "") {
                console.log("Clear for current");
                clear();
                loaded = false;
            } else {
                if (!loaded) {
                    console.log("Loading for: " + uri);
                    load();
                } 
            }
            return view().content;
        };
    })
});