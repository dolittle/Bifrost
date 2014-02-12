Bifrost.namespace("Bifrost.views", {
    viewModelManager: Bifrost.Singleton(function(assetsManager, documentService, viewModelLoader, regionManager, taskFactory, MasterViewModel) {
        var self = this;
        this.assetsManager = assetsManager;
        this.viewModelLoader = viewModelLoader;
        this.documentService = documentService;

        this.masterViewModel = MasterViewModel;

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
            var promise = Bifrost.execution.Promise.create();
            var task = taskFactory.createViewModelApplier(view, self.masterViewModel);

            var region = documentService.getRegionFor(view.element);
            region.tasks.execute(task).continueWith(function (instance) {
                promise.signal(instance);
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
            var task = taskFactory.createViewModelsApplier(root, self.masterViewModel);
            documentService.getRegionFor(root).tasks.execute(task).continueWith(function () {
            });
        };

        this.loadAndApplyAllViewModelsInDocument = function () {
            self.masterViewModel = Bifrost.views.MasterViewModel.create();
            self.loadAndApplyAllViewModelsWithinElement(self.documentService.DOMRoot);
        };
    })
});