Bifrost.namespace("Bifrost.views", {
    ViewBindingHandlerTemplateEngine: Bifrost.Type.extend(function(engine, element, viewUri, allBindingsAccessor) {
        var templateSource = Bifrost.views.ViewBindingHandlerTemplateSource.create({
            element: element,
            viewUri: viewUri,
            allBindingsAccessor : allBindingsAccessor,
        });

        engine.renderTemplate = function (template, bindingContext, options) {
            var renderedTemplateSource = engine.renderTemplateSource(templateSource, bindingContext, options);
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