Bifrost.namespace("Bifrost.views", {
    viewModelManager: Bifrost.Singleton(function(assetsManager, documentService, viewModelLoader) {
        var self = this;
        this.assetsManager = assetsManager;
        this.viewModelLoader = viewModelLoader;
        this.documentService = documentService;

        function applyViewModel(instance, target) {
            var viewModelFile = self.documentService.getViewModelFileFrom(target);
            ko.applyBindingsToNode(target, {
                'with': instance
            });
            self.documentService.setViewModelOn(target, instance);
            if (typeof instance.activated == "function") {
                instance.activated();
            }
        }

        function applyViewModelsByAttribute(path, container) {
            var viewModelApplied = false;

            var elements = self.documentService.getAllElementsWithViewModelFilesFrom(container);
            elements.forEach(function(target) {
                viewModelApplied = true;
                var viewModelFile = $(this).data("viewmodel-file");
                self.viewModelLoader.load(viewModelFile, path).continueWith(function (instance) {
                    applyViewModelInMemory(scriptFile, function (instance) {
                        applyViewModel(instance, target, viewModelFile);
                    });
                });
            });

            return viewModelApplied;
        }

        function applyViewModelByConventionFromPath(path, container) {
            if (self.hasForView(path)) {
                var viewModelFile = Bifrost.path.changeExtension(path, "js");
                self.documentService.setViewModelFileOn(container, viewModelFile);

                self.getForView(path).continueWith(function (instance) {
                    applyViewModel(instance, container);
                });
            }
        }

        function applyViewModelInMemory(path, callback) {
            var localPath = Bifrost.path.getPathWithoutFilename(path);
            var filename = Bifrost.path.getFilenameWithoutExtension(path);
            var wasInMemory = false;

            for (var mapperKey in Bifrost.namespaceMappers) {
                var mapper = Bifrost.namespaceMappers[mapperKey];
                if (typeof mapper.hasMappingFor === "function" && mapper.hasMappingFor(path)) {
                    var namespacePath = mapper.resolve(localPath);
                    var namespace = Bifrost.namespace(namespacePath);

                    if (filename in namespace) {
                        wasInMemory = true;
                        namespace[filename].beginCreate().continueWith(function (instance) {
                            if (typeof callback != null) {
                                callback(instance);
                            }
                        });
                    }
                }
            }

            return wasInMemory;
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

        this.getForView = function (viewPath) {
            var scriptFile = Bifrost.path.changeExtension(viewPath, "js");
            return self.viewModelLoader.load(scriptFile).continueWith(function () {
                applyViewModelInMemory(scriptFile);
            });
        };

        this.applyToViewIfAny = function (view) {
            var viewModelApplied = false;

            viewModelApplied = applyViewModelInMemory(view.path, function (instance) {
                var viewModelFile = Bifrost.path.changeExtension(view.path, "js");
                self.documentService.setViewModelFileOn(view.element,viewModelFile);
                applyViewModel(instance, view.element);
            });
            if (viewModelApplied == false) {
                viewModelApplied = applyViewModelsByAttribute(view.path, view.element);
                if (viewModelApplied == false) {
                    applyViewModelByConventionFromPath(view.path, view.element);
                }
            }
        };

        this.loadAndApplyAllViewModelsInDocument = function () {
        };
    })
});