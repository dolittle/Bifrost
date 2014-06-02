Bifrost.namespace("Bifrost.views", {
    ViewBindingHandlerTemplateEngine: Bifrost.Type.extend(function (engine, viewModelManager, element, viewUri, allBindingsAccessor) {
        engine.templateSource = Bifrost.views.ViewBindingHandlerTemplateSource.create({
            element: element,
            viewUri: viewUri,
            allBindingsAccessor: allBindingsAccessor
        });

        engine.renderTemplate = function (template, bindingContext, options) {
            engine.templateSource.currentElement = template;
            
            if (!Bifrost.isNullOrUndefined(bindingContext.$viewModel)) {
                bindingContext.$data = bindingContext.$viewModel;
                bindingContext.$root = bindingContext.$viewModel;
            }

            var renderedTemplateSource = engine.renderTemplateSource(engine.templateSource, bindingContext, options);
            return renderedTemplateSource;
        }
    }),
    viewBindingHandlerTemplateEngine: {
        create: function (element, viewUri, allBindingsAccessor) {
            var engine = new ko.nativeTemplateEngine();
            Bifrost.views.ViewBindingHandlerTemplateEngine.create({
                engine: engine,
                element: element,
                viewUri: viewUri,
                allBindingsAccessor: allBindingsAccessor
            });
            return engine;
        }
    }
})