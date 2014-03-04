Bifrost.namespace("Bifrost.views", {
    ViewBindingHandlerTemplateSource: Bifrost.Type.extend(function (UIManager, viewModelManager, viewModelTypes, documentService, pathResolvers, viewFactory, regionManager, element, viewUri, allBindingsAccessor) {
        var self = this;
        var loaded = false;
        var nullView = viewFactory.createFrom("");
        nullView.content = "<span></span>";
        var view = ko.observable(nullView);
        var firstTime = true;

        function load() {
            loaded = true;
            var uri = ko.utils.unwrapObservable(viewUri);

            var viewModelParameters = allBindingsAccessor().viewModelParameters || null;
            var actualPath = pathResolvers.resolve(element, uri);
            var actualView = viewFactory.createFrom(actualPath);
            actualView.element = element;

            regionManager.getFor(actualView).continueWith(function (region) {

                actualView.load(region).continueWith(function (loadedView) {
                    viewModelTypes.beginCreateInstanceOfViewModel(loadedView.viewModelPath, region, viewModelParameters).continueWith(function (viewModel) {
                        if (!Bifrost.isNullOrUndefined(viewModel)) {
                            region.viewModel = viewModel;
                        }

                        var wrapper = document.createElement("div");
                        wrapper.innerHTML = loadedView.content;

                        UIManager.handle(wrapper);

                        loadedView.content = wrapper.innerHTML;

                        view(loadedView);
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

        this.createAndSetViewModelFor = function (bindingContext) {
            if (firstTime == true) {
                firstTime = false;
                return;
            }
            if (!Bifrost.isNullOrUndefined(view()) && !Bifrost.isNullOrUndefined(view().viewModelType)) {
                var region = view().region;
                var viewModelParameters = allBindingsAccessor().viewModelParameters || {};
                viewModelParameters.region = region;

                var lastRegion = Bifrost.views.Region.current;
                Bifrost.views.Region.current = region;
                var viewModel = view().viewModelType.create(viewModelParameters);
                bindingContext.$data = viewModel;
                Bifrost.views.Region.current = lastRegion;
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