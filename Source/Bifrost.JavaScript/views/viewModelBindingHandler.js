Bifrost.namespace("Bifrost.views", {
    viewModelBindingHandler: Bifrost.Type.extend(function(viewManager, documentService) {
        this.init = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
        };
        this.update = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
        };
    })
});
Bifrost.views.viewModelBindingHandler.initialize = function () {
    ko.bindingHandlers.viewModel = Bifrost.views.viewModelBindingHandler.create();
};

