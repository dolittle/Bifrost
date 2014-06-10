Bifrost.namespace("Bifrost.views", {
    ViewBindingHandlerTemplateEngine: Bifrost.Type.extend(function (viewModelManager, regionManager) {
        var self = this;
        this.renderTemplate = function (template, bindingContext, options) {
            var templateSource;
            if (Bifrost.isNullOrUndefined(options.element.templateSource)) {
                templateSource = Bifrost.views.ViewBindingHandlerTemplateSource.create({
                    viewUri: options.viewUri,
                    region: options.region
                });
                options.element.templateSource = templateSource;
            } else {
                templateSource = options.element.templateSource;
            }

            if (Bifrost.isNullOrUndefined(options.element.view)) {
                templateSource.loadFor(options.element, options.view, options.region).continueWith(function (view) {
                    options.element.view = view;
                    regionManager.describe(options.view, options.region).continueWith(function () {
                        try {
                            // This is a bit dodgy, but can't find any way around it
                            // Put an empty object for dependency detection - we don't want Knockout to be observing any observables on our viewModel
                            ko.dependencyDetection.begin();

                            if (!Bifrost.isNullOrUndefined(view.viewModelType)) {
                                var viewModelParameters = options.viewModelParameters;
                                viewModelParameters.region = options.region;
                                var instance = view.viewModelType.create(viewModelParameters);
                                options.element.viewModel = instance;
                                options.data(instance);

                                bindingContext.$data = instance;
                            }
                        } finally {
                            ko.dependencyDetection.end();
                        }
                    });
                });
            }

            bindingContext.$root = bindingContext.$data;
            var renderedTemplateSource = self.renderTemplateSource(templateSource, bindingContext, options);
            return renderedTemplateSource;
        };
    })
});

(function () {
    var nativeTemplateEngine = new ko.nativeTemplateEngine();
    var baseCreate = Bifrost.views.ViewBindingHandlerTemplateEngine.create;
    Bifrost.views.ViewBindingHandlerTemplateEngine.create = function () {
        var instance = baseCreate.call(Bifrost.views.ViewBindingHandlerTemplateEngine, arguments);

        for (var property in nativeTemplateEngine) {
            if (!instance.hasOwnProperty(property)) {
                instance[property] = nativeTemplateEngine[property];
            }
        }

        return instance;
    };
})();
