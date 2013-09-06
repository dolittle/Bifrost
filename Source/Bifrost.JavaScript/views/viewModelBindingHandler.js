Bifrost.namespace("Bifrost.views", {
    viewModelBindingHandler: Bifrost.Type.extend(function(viewManager, documentService) {
        var self = this;
        this.viewManager = viewManager;
        this.documentService = documentService;

        this.init = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
            var viewModel = self.documentService.getViewModelFrom(element);
            var childBindingContext = bindingContext.createChildContext(viewModel);
            childBindingContext.$root = viewModel;
            ko.applyBindingsToDescendants(childBindingContext, element);
            return { controlsDescendantBindings: true };
        };
        this.update = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
        };
    })
});
Bifrost.views.viewModelBindingHandler.initialize = function () {
    ko.bindingHandlers.viewModel = Bifrost.views.viewModelBindingHandler.create();
};

