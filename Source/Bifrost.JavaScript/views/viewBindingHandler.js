Bifrost.namespace("Bifrost.views", {

    viewBindingHandler: Bifrost.Type.extend(function (UIManager, viewManager, viewModelManager, documentService, regionManager) {
        var self = this;

        var templateEnginesByUri = {};

        function getTemplateEngineFor(viewUri, element, allBindingsAccessor) {
            var uri = ko.utils.unwrapObservable(viewUri);
            if (Bifrost.isNullOrUndefined(uri)) return null;

            var key = uri;
            if (templateEnginesByUri.hasOwnProperty(key)) {
                return templateEnginesByUri[key];
            } else {
                var engine = Bifrost.views.viewBindingHandlerTemplateEngine.create(element, viewUri, allBindingsAccessor);
                templateEnginesByUri[key] = engine;
                return engine;
            }
        }

        function createViewModel(element, view, viewModelParameters, retainViewModel) {
            if (!Bifrost.isNullOrUndefined(viewModelManager.masterViewModel.getFor(element)) &&
                documentService.hasOwnRegion(element)) {
                regionManager.evict(element.region);
                documentService.clearRegionOn(element);
            }

            if (retainViewModel === true && !Bifrost.isNullOrUndefined(element.previousViewModel)) {
                return element.previousViewModel;
            }

            var region = view.region;

            viewModelParameters.region = region;

            var lastRegion = Bifrost.views.Region.current;
            Bifrost.views.Region.current = region;

            var viewModel = null;
            var viewModelType = view.viewModelType;
            if (!Bifrost.isNullOrUndefined(viewModelType)) {
                viewModel = view.viewModelType.create(viewModelParameters);
            }
            var loadedView = view;

            if (!Bifrost.isNullOrUndefined(viewModel)) {
                region.viewModel = viewModel;
            }

            if (retainViewModel === true) {
                element.previousViewModel = viewModel;
            }
            Bifrost.views.Region.current = lastRegion;            

            return viewModel;
        };


        function makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor, bindingContext) {
            return function () {
                var viewUri = valueAccessor();
                var viewModel = ko.observable();
                var viewModelParameters = allBindingsAccessor().viewModelParameters || {};
                var retainViewModel = allBindingsAccessor().retainViewModel || false;

                var templateEngine = getTemplateEngineFor(viewUri, element, allBindingsAccessor);

                if (!Bifrost.isNullOrUndefined(templateEngine)) {
                    templateEngine.templateSource.viewLoaded.subscribe(function (view) {
                        var vm = createViewModel(element, view, viewModelParameters, retainViewModel);
                        bindingContext.$viewModel = vm;
                        viewModel(vm);
                        viewModelManager.masterViewModel.setFor(element, vm)
                    });
                }

                return {
                    if: true,
                    data: viewModel,
                    templateEngine: templateEngine,
                    viewModelParameters : viewModelParameters
                }
            };
        };
        
        this.init = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            //return ko.bindingHandlers.template.init(element, makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor, bindingContext), allBindingsAccessor, viewModel, bindingContext);
        };

        this.update = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            return ko.bindingHandlers.template.update(element, makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor, bindingContext), allBindingsAccessor, viewModel, bindingContext);
        };
    })
});
Bifrost.views.viewBindingHandler.initialize = function () {
    ko.bindingHandlers.view = Bifrost.views.viewBindingHandler.create();
    ko.jsonExpressionRewriting.bindingRewriteValidators.view = false; // Can't rewrite control flow bindings
    ko.virtualElements.allowedBindings.view = true;
};