Bifrost.namespace("Bifrost.views", {
    viewModelBindingHandler: Bifrost.Type.extend(function(viewManager, documentService) {
        var self = this;
        this.viewManager = viewManager;
        this.documentService = documentService;

        this.init = function (element, valueAccessor, allBindingAccessor, viewMode, bindingContext) {
            var viewModel = self.documentService.getViewModelFrom(element);
            var childBindingContext = bindingContext.createChildContext(viewModel);
            ko.applyBindingsToDescendants(childBindingContext, element);
            return { controlsDescendantBindings: true };
        };
        this.update = function (element, valueAccessor, allBindingAccessor, viewModel, bindingContext) {
        };
    })
});
Bifrost.views.viewModelBindingHandler.initialize = function () {
    ko.bindingHandlers.viewModel = Bifrost.views.viewModelBindingHandler.create();
};

