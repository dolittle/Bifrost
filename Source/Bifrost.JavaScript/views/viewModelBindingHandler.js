Bifrost.namespace("Bifrost.views", {
    viewModelBindingHandler: Bifrost.Type.extend(function(viewFactory, viewModelLoader, viewModelManager, viewModelTypes, regionManager) {
        this.init = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
            return { controlsDescendantBindings: true };
        };
        this.update = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
            var view = viewFactory.createFrom("/");
            view.content = element.innerHTML;
            var path = ko.utils.unwrapObservable(valueAccessor());

            regionManager.getFor(view).continueWith(function () {
                var viewModelParameters = allBindingsAccessor().viewModelParameters || {};
                viewModelParameters.region = region;

                if (viewModelTypes.isLoaded(path)) {
                    viewModelType = viewModelTypes.getViewModelTypeForPath(path);
                    var viewModel = viewModelType.create(viewModelParameters);
                    var childBindingContext = bindingContext.createChildContext(viewModel);
                    ko.applyBindingsToDescendants(childBindingContext, element);
                } else {
                    viewModelLoader.load(path, region, viewModelParameters).continueWith(function (viewModel) {
                        var childBindingContext = bindingContext.createChildContext(viewModel);
                        ko.applyBindingsToDescendants(childBindingContext, element);
                    });
                }
            });
        };
    })
});
Bifrost.views.viewModelBindingHandler.initialize = function () {
    ko.bindingHandlers.viewModel = Bifrost.views.viewModelBindingHandler.create();
};

