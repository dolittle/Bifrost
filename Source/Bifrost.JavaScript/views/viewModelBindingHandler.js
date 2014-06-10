Bifrost.namespace("Bifrost.views", {
    viewModelBindingHandler: Bifrost.Type.extend(function(documentService, viewFactory, viewModelLoader, viewModelManager, viewModelTypes, regionManager) {
        this.init = function (element, valueAccessor, allBindingsAccessor, parentViewModel, bindingContext) {
            return { controlsDescendantBindings: true };
        };
        this.update = function (element, valueAccessor, allBindingsAccessor, parentViewModel, bindingContext) {
            var path = ko.utils.unwrapObservable(valueAccessor());
            if (element._isLoading === true || (element._viewModelPath === path && !Bifrost.isNullOrUndefined(element._viewModel))) return;

            element._isLoading = true;
            element._viewModelPath = path;

            var viewPath = "/";

            if( documentService.hasViewFile(element) ) {
                viewPath = documentService.getViewFileFrom(element);
            }

            var view = viewFactory.createFrom(viewPath);
            view.content = element.innerHTML;
            view.element = element;


            var region = regionManager.getFor(view);
            regionManager.describe(view,region).continueWith(function (region) {
                var viewModelParameters = allBindingsAccessor().viewModelParameters || {};
                viewModelParameters.region = region;

                if (viewModelTypes.isLoaded(path)) {
                    viewModelType = viewModelTypes.getViewModelTypeForPath(path);

                    var lastRegion = Bifrost.views.Region.current;
                    Bifrost.views.Region.current = region;

                    viewModelType.beginCreate(viewModelParameters).continueWith(function (viewModel) {
                        var childBindingContext = bindingContext.createChildContext(viewModel);
                        childBindingContext.$root = viewModel;
                        element._viewModel = viewModel;

                        ko.applyBindingsToDescendants(childBindingContext, element);
                        Bifrost.views.Region.current = lastRegion;

                        element._isLoading = false;
                    }).onFail(function(e) {
                        console.log("Could not create an instance of '" + viewModelType._namespace.name + "." + viewModelType._name+" - Reason : "+e);
                    });
                } else {
                    viewModelLoader.load(path, region, viewModelParameters).continueWith(function (viewModel) {
                        var childBindingContext = bindingContext.createChildContext(viewModel);
                        childBindingContext.$root = viewModel;
                        element._viewModel = viewModel;
                        ko.applyBindingsToDescendants(childBindingContext, element);

                        element._isLoading = false;
                    });
                }
            });
            return { controlsDescendantBindings: true };
        };
    })
});
Bifrost.views.viewModelBindingHandler.initialize = function () {
    ko.bindingHandlers.viewModel = Bifrost.views.viewModelBindingHandler.create();
};

