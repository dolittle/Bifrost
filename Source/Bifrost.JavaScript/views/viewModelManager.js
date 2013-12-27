Bifrost.namespace("Bifrost.views", {
    viewModelManager: Bifrost.Singleton(function(assetsManager, documentService, viewModelLoader, regionManager) {
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
            if (elements.length > 0) {

                function loadAndApply(target) {
                    viewModelApplied = true;
                    var viewModelFile = $(target).data("viewmodel-file");
                    self.viewModelLoader.load(viewModelFile).continueWith(function (instance) {
                        applyViewModel(instance, target, viewModelFile);
                        instance.region.viewModel = instance;
                    });
                }

                if (elements.length == 1) {
                    loadAndApply(elements[0]);
                } else {
                    for (var elementIndex = elements.length - 1; elementIndex > 0; elementIndex--) {
                        loadAndApply(elements[elementIndex]);
                    }
                }
            }

            return viewModelApplied;
        }

        function applyViewModelByConventionFromPath(path, container) {
            if (self.hasForView(path)) {
                var viewModelFile = Bifrost.Path.changeExtension(path, "js");
                self.documentService.setViewModelFileOn(container, viewModelFile);

                self.viewModelLoader.load(viewModelFile).continueWith(function (instance) {
                    applyViewModel(instance, target, viewModelFile);
                    instance.region.viewModel = instance;
                });
            }
        }

        this.hasForView = function (viewPath) {
            var scriptFile = Bifrost.Path.changeExtension(viewPath, "js");
            scriptFile = Bifrost.Path.makeRelative(scriptFile);
            var hasViewModel = self.assetsManager.hasScript(scriptFile);
            return hasViewModel;
        };

        this.getViewModelPathForView = function (viewPath) {
            var scriptFile = Bifrost.Path.changeExtension(viewPath, "js");
            return scriptFile;
        };

        this.applyToViewIfAny = function (view) {
            var viewModelApplied = false;

            var promise = Bifrost.execution.Promise.create();

            regionManager.getFor(view).continueWith(function (region) {
                var previousRegion = Bifrost.views.Region.current;
                Bifrost.views.Region.current = region;

                if (self.hasForView(view.path)) {
                    var viewModelFile = Bifrost.Path.changeExtension(view.path, "js");
                    self.documentService.setViewModelFileOn(view.element, viewModelFile);

                    self.viewModelLoader.load(viewModelFile, region).continueWith(function (instance) {
                        applyViewModel(instance, view.element);
                        region.viewModel = instance;
                        promise.signal();
                    });
                } else {
                    viewModelApplied = applyViewModelsByAttribute(view.path, view.element, region);
                    if (viewModelApplied == false) {
                        applyViewModelByConventionFromPath(view.path, view.element, region);
                    }
                    promise.signal();
                }

                Bifrost.views.Region.current = previousRegion;
            });

            return promise;
        };

        this.isLoaded = function (path) {
            var localPath = Bifrost.Path.getPathWithoutFilename(path);
            var filename = Bifrost.Path.getFilenameWithoutExtension(path);
            var namespacePath = Bifrost.namespaceMappers.mapPathToNamespace(localPath);
            if (namespacePath != null) {
                var namespace = Bifrost.namespace(namespacePath);

                if (filename in namespace) {
                    return true;
                }
            }
            return false;
        };

        this.loadAndApplyAllViewModelsWithinElement = function (root) {
            var elements = self.documentService.getAllElementsWithViewModelFilesFrom(root);
            var loadedViewModels = 0;

            elements.forEach(function (element) {
                var viewModelFile = self.documentService.getViewModelFileFrom(element);
                var viewFile = self.documentService.getViewFileFrom(element);

                var view = Bifrost.views.View.create({
                    viewLoader: {
                        load: function () {
                            var promise = Bifrost.execution.Promise.create();
                            promise.signal(element.innerHTML);
                            return promise;
                        }
                    },
                    path: viewFile
                });
                view.element = element;
                view.content = element.innerHTML;

                regionManager.getFor(view).continueWith(function (region) {
                    self.viewModelLoader.load(viewModelFile, region).continueWith(function (instance) {
                        documentService.setViewModelOn(element, instance);

                        loadedViewModels++;

                        if (loadedViewModels == elements.length) {
                            elements.forEach(function (elementToApplyBindingsTo) {
                                var viewModel = self.documentService.getViewModelFrom(elementToApplyBindingsTo);
                                setViewModelBindingExpression(viewModel, elementToApplyBindingsTo);
                            });


                            if (!documentService.pageHasViewModel(self.masterViewModel)) {
                                ko.applyBindings(self.masterViewModel);
                            } else {
                                ko.applyBindings(instance, element);
                            }
                        }
                    });
                });
            });
        };

        this.loadAndApplyAllViewModelsInDocument = function () {
            self.masterViewModel = {};
            self.loadAndApplyAllViewModelsWithinElement(self.documentService.DOMRoot);
        };
    })
});