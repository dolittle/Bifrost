Bifrost.namespace("Bifrost.views", {
    viewModelManager: Bifrost.Singleton(function(assetsManager, documentService, viewModelLoader) {
        var self = this;
        this.assetsManager = assetsManager;
        this.viewModelLoader = viewModelLoader;
        this.documentService = documentService;

        this.masterViewModel = {};


        function applyViewModel(instance, target) {
            var viewModelFile = self.documentService.getViewModelFileFrom(target);
            self.documentService.setViewModelOn(target, instance);
            
            ko.applyBindingsToNode(target, {
                'viewModel': instance
            });

            if (typeof instance.activated == "function") {
                instance.activated();
            }
        }

        function setViewModelBindingExpression(instance, target) {
            var viewModelFile = self.documentService.getViewModelFileFrom(target);
            self.documentService.setViewModelOn(target, instance);

            if (viewModelFile.indexOf(".") > 0) {
                viewModelFile = viewModelFile.substr(0, viewModelFile.indexOf("."));
            }

            var propertyName = viewModelFile.replaceAll("/", "");
            self.masterViewModel[propertyName] = ko.observable(instance);

            $(target).attr("data-bind", "viewModel: $data." + propertyName);
            
            if (typeof instance.activated == "function") {
                instance.activated();
            }
        }

        function applyViewModelsByAttribute(path, container) {
            var viewModelApplied = false;

            var elements = self.documentService.getAllElementsWithViewModelFilesFrom(container);
            elements.forEach(function(target) {
                viewModelApplied = true;
                var viewModelFile = $(target).data("viewmodel-file");
                self.viewModelLoader.load(viewModelFile, path).continueWith(function (instance) {
                    applyViewModel(instance, target, viewModelFile);
                });
            });

            return viewModelApplied;
        }

        function applyViewModelByConventionFromPath(path, container) {
            if (self.hasForView(path)) {
                var viewModelFile = Bifrost.path.changeExtension(path, "js");
                self.documentService.setViewModelFileOn(container, viewModelFile);

                self.viewModelLoader.load(viewModelFile, path).continueWith(function (instance) {
                    applyViewModel(instance, target, viewModelFile);
                });
            }
        }

        this.hasForView = function (viewPath) {
            var scriptFile = Bifrost.path.changeExtension(viewPath, "js");
            scriptFile = Bifrost.path.makeRelative(scriptFile);
            var hasViewModel = self.assetsManager.hasScript(scriptFile);
            return hasViewModel;
        };

        this.getViewModelPathForView = function (viewPath) {
            var scriptFile = Bifrost.path.changeExtension(viewPath, "js");
            return scriptFile;
        };

        this.applyToViewIfAny = function (view) {
            var viewModelApplied = false;

            if (self.hasForView(view.path)) {
                var viewModelFile = Bifrost.path.changeExtension(view.path, "js");
                self.documentService.setViewModelFileOn(view.element,viewModelFile);

                self.viewModelLoader.load(viewModelFile).continueWith(function (instance) {
                    applyViewModel(instance, view.element);
                });
            } else {
                viewModelApplied = applyViewModelsByAttribute(view.path, view.element);
                if (viewModelApplied == false) {
                    applyViewModelByConventionFromPath(view.path, view.element);
                }
            }
        };

        this.loadAndApplyAllViewModelsWithinElement = function (root) {
            var elements = self.documentService.getAllElementsWithViewModelFilesFrom(root);
            var loadedViewModels = 0;

            self.masterViewModel = {};

            elements.forEach(function (element) {
                var viewModelFile = self.documentService.getViewModelFileFrom(element);

                self.viewModelLoader.load(viewModelFile).continueWith(function (instance) {
                    documentService.setViewModelOn(element, instance);

                    loadedViewModels++;

                    if (loadedViewModels == elements.length) {
                        elements.forEach(function (elementToApplyBindingsTo) {
                            var viewModel = self.documentService.getViewModelFrom(elementToApplyBindingsTo);
                            setViewModelBindingExpression(viewModel, elementToApplyBindingsTo);
                        });

                        ko.applyBindings(self.masterViewModel);
                    }
                });
            });
        };

        this.loadAndApplyAllViewModelsInDocument = function () {
            self.loadAndApplyAllViewModelsWithinElement(self.documentService.DOMRoot);
        };
    })
});