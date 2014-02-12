Bifrost.namespace("Bifrost.views", {
    viewBindingHandler: Bifrost.Type.extend(function (viewManager, viewRenderers, pathResolvers, viewFactory, viewModelManager, documentService) {
        var self = this;
        this.init = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
            return { controlsDescendantBindings: true };
        };
        this.update = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
            var uri = ko.utils.unwrapObservable(valueAccessor());
            if (Bifrost.isNullOrUndefined(uri) || uri === "") {
                documentService.cleanChildrenOf(element);
                element.innerHTML = "";
                documentService.setViewUriOn(element, null);
            } else {
                var existingUri = documentService.getViewUriFrom(element);
                if (existingUri !== uri) {
                    documentService.setViewUriOn(element, uri);
                    var viewModelParameters = allBindingAccessor().viewModelParameters || null;
                    documentService.setViewModelParametersOn(element, viewModelParameters);

                    viewManager.render(element).continueWith(function () { /* Nothingness */ });
                }
            }
        };
    })
});
Bifrost.views.viewBindingHandler.initialize = function () {
    ko.bindingHandlers.view = Bifrost.views.viewBindingHandler.create();
};

