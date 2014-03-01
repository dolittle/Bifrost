Bifrost.namespace("Bifrost.views", {
    ViewBindingHandlerTemplateEngine: Bifrost.Type.extend(function(engine, element, viewUri, allBindingsAccessor) {
        var templateSource = Bifrost.views.ViewBindingHandlerTemplateSource.create({
            element: element,
            viewUri: viewUri,
            allBindingsAccessor: allBindingsAccessor
        });

        engine.renderTemplate = function (template, bindingContext, options) {
            templateSource.bindingContext = bindingContext;

            /*
            templateSource.setRootOnBindingContext = function (viewModel) {
                bindingContext.$root = "ASADS";
            };*/

            /*
            setTimeout(function () {
                bindingContext.$root = "ASADS";
                console.log("SET");
            }, 5000);*/
            
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