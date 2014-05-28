Bifrost.namespace("Bifrost.views", {
    ViewBindingHandlerTemplateEngine: Bifrost.Type.extend(function(engine, viewModelManager, element, viewUri, allBindingsAccessor) {
        var templateSource = Bifrost.views.ViewBindingHandlerTemplateSource.create({
            element: element,
            viewUri: viewUri,
            allBindingsAccessor: allBindingsAccessor
        });

        engine.renderTemplate = function (template, bindingContext, options) {
            console.log("Render template for: " + template.attributes["data-bind"].value);
            templateSource.currentElement = template;
            templateSource.createAndSetViewModelFor(bindingContext, options.viewModelParameters);

            var renderedTemplateSource = engine.renderTemplateSource(templateSource, bindingContext, options);
            console.log("Rendered template: " + renderedTemplateSource);

            if (!Bifrost.isNullOrUndefined(bindingContext.$data)) {
                bindingContext.$root = bindingContext.$data;
            }
            
            viewModelManager.masterViewModel.setFor(element, bindingContext.$data);

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