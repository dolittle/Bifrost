Bifrost.namespace("Bifrost.views", {

    viewBindingHandler: Bifrost.Type.extend(function (UIManager, viewManager, viewModelManager, documentService) {
        var self = this;

        var templateEnginesByUri = {};

        function getTemplateEngineFor(viewUri, element, allBindingsAccessor) {
            var uri = ko.utils.unwrapObservable(viewUri);

            if (templateEnginesByUri.hasOwnProperty(uri)) {
                return templateEnginesByUri[uri];
            } else {
                var engine = Bifrost.views.viewBindingHandlerTemplateEngine.create(element, viewUri, allBindingsAccessor);
                templateEnginesByUri[uri] = engine;
                return engine;
            }
        }

        function makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor) {
            return function () {
                var viewUri = valueAccessor();
                var viewModel = viewModelManager.masterViewModel.getFor(element);
                return {
                    if: true,
                    data: viewModel,
                    templateEngine: getTemplateEngineFor(viewUri, element, allBindingsAccessor)
                }
            };
        };

        this.init = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            return ko.bindingHandlers.template.init(element, makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor), allBindingsAccessor, viewModel, bindingContext);
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