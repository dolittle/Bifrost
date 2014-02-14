Bifrost.namespace("Bifrost.views", {
    viewModelBindingHandler: Bifrost.Type.extend(function(viewManager, documentService) {
        var self = this;
        this.viewManager = viewManager;
        this.documentService = documentService;

        this.init = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
            console.log("ViewModel init");

            //console.log("ValueAccessor : " + valueAccessor());
            
            //var childBindingContext = bindingContext.extend(valueAccessor);

            var viewModel = self.documentService.getViewModelFrom(element);
            var childBindingContext = bindingContext.createChildContext(viewModel)
            childBindingContext.$root = viewModel;
            if (ko.isObservable(valueAccessor())) {
                valueAccessor().subscribe(function (context) {
                    console.log("ViewModel was Updated");
                    //childBindingContext.$root = context;
                });
                
            }

            ko.applyBindingsToDescendants(childBindingContext, element);
            return { controlsDescendantBindings: true };
        };
        this.update = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
            console.log("ViewModel update");
            var viewModel = valueAccessor();
        };
    })
});
Bifrost.views.viewModelBindingHandler.initialize = function () {
    ko.bindingHandlers.viewModel = Bifrost.views.viewModelBindingHandler.create();
};

