Bifrost.namespace("Bifrost.views", {
    viewBindingHandler: Bifrost.Type.extend(function (ViewBindingHandlerTemplateEngine, UIManager, viewManager, viewModelManager, documentService, regionManager) {
        var self = this;

        function makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor, bindingContext) {
            return function () {
                var viewUri = valueAccessor();
                var viewModel = ko.observable(element.viewModel);
                var viewModelParameters = allBindingsAccessor().viewModelParameters || {};
                var retainViewModel = allBindingsAccessor().retainViewModel || false;
                var region = documentService.getRegionFor(element);
                var templateEngine = null;

                if (Bifrost.isNullOrUndefined(viewUri) || viewUri == "") {
                    templateEngine = new ko.nativeTemplateEngine();
                } else {
                    templateEngine = ViewBindingHandlerTemplateEngine;
                }

                return {
                    if: true,
                    data: viewModel,
                    element: element,
                    templateEngine: templateEngine,
                    viewUri: viewUri,
                    viewModelParameters: viewModelParameters,
                    region: region
                }
            };
        };
        
        this.init = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            return { controlsDescendantBindings: true };
        };

        this.update = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            console.log("Update");
            return ko.bindingHandlers.template.update(element, makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor, bindingContext), allBindingsAccessor, viewModel, bindingContext);
        };
    })
});
Bifrost.views.viewBindingHandler.initialize = function () {
    ko.bindingHandlers.view = Bifrost.views.viewBindingHandler.create();
    ko.jsonExpressionRewriting.bindingRewriteValidators.view = false; // Can't rewrite control flow bindings
    ko.virtualElements.allowedBindings.view = true;
};