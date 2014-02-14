Bifrost.namespace("Bifrost.views", {
    viewBindingHandlerTemplateEngine: function(documentService, viewManager, element, value, allBindingsAccessor) {
        var engine = new ko.nativeTemplateEngine();

        engine.renderTemplate = function (template, bindingContext, options) {
            var uri = ko.utils.unwrapObservable(value);
            if (Bifrost.isNullOrUndefined(uri) || uri === "") {
                viewModelManager.masterViewModel.clearFor(element);
                documentService.cleanChildrenOf(element);
                element.innerHTML = "";
                documentService.setViewUriOn(element, null);
            } else {
                var existingUri = documentService.getViewUriFrom(element);
                if (existingUri !== uri) {
                    /*
                    console.log("Clear on masterViewModel");
                    viewModelManager.masterViewModel.clearFor(element);
                    console.log("Clean children");
                    documentService.cleanChildrenOf(element);*/
                    console.log("Set view Uri");
                    documentService.setViewUriOn(element, uri);


                    var viewModelParameters = allBindingsAccessor().viewModelParameters || null;
                    documentService.setViewModelParametersOn(element, viewModelParameters);

                    console.log("Render view");
                    viewManager.render(element).continueWith(function () {
                        var view = element.view;
                        /* Nothingness */
                    });
                }
            }

            var d = ko.utils.parseHtmlFragment("<!-- loading -->");
            return d;

        }

        return engine;
    },


    viewBindingHandler: Bifrost.Type.extend(function (viewManager, viewRenderers, pathResolvers, viewFactory, viewModelManager, documentService) {
        var self = this;

        function makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor) {
            return function () {
                var value = valueAccessor();
                return {
                    if: value,
                    data: value,
                    templateEngine: new Bifrost.views.viewBindingHandlerTemplateEngine(documentService, viewManager, element, value, allBindingsAccessor)
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


