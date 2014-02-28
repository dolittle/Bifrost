Bifrost.namespace("Bifrost.views", {

    viewBindingHandler: Bifrost.Type.extend(function (UIManager, viewManager, viewModelManager, documentService) {
        var self = this;

        var templateEnginesByUri = {};

        function getTemplateEngineFor(viewUri, element, allBindingsAccessor) {
            if (templateEnginesByUri.hasOwnProperty(viewUri)) {
                return templateEnginesByUri[viewUri];
            } else {
                var engine = Bifrost.views.viewBindingHandlerTemplateEngine.create(element, viewUri, allBindingsAccessor);
                templateEnginesByUri[viewUri] = engine;
                return engine;
            }
        }

        function makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor) {
            return function () {
                var viewUri = valueAccessor();
                var viewModelObservable = viewModelManager.masterViewModel.getViewModelObservableFor(element);
                return {
                    if: true,
                    data: viewModelObservable,
                    templateEngine: getTemplateEngineFor(viewUri, element, allBindingsAccessor)
                }
            };
        };

        this.init = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            return ko.bindingHandlers.template.init(element, makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor));
        };
        this.update = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            return ko.bindingHandlers.template.update(element, makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor), allBindingsAccessor, viewModel, bindingContext);
        };
    })
});
Bifrost.views.viewBindingHandler.initialize = function () {
    ko.bindingHandlers.view = Bifrost.views.viewBindingHandler.create();
    ko.jsonExpressionRewriting.bindingRewriteValidators.view = false; // Can't rewrite control flow bindings
    ko.virtualElements.allowedBindings.view = true;
};


