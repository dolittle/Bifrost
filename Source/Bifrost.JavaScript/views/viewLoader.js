Bifrost.namespace("Bifrost.views", {
    viewLoader: Bifrost.Singleton(function (viewModelManager, viewModelLoader, taskFactory, fileFactory, regionManager) {
        this.load = function (path,region) {
            var promise = Bifrost.execution.Promise.create();

            var files = [];

            var viewFile = fileFactory.create(path, Bifrost.io.fileType.html);
            if (path.indexOf("?") > 0) {
                viewFile.path.fullPath = viewFile.path.fullPath + path.substr(path.indexOf("?"));
            }
            files.push(viewFile);

            var viewModelPath = null;
            if (viewModelManager.hasForView(path)) {
                viewModelPath = viewModelManager.getViewModelPathForView(path);
                if (!viewModelManager.isLoaded(viewModelPath)) {
                    var viewModelFile = fileFactory.create(viewModelPath, Bifrost.io.fileType.javaScript);
                    files.push(viewModelFile);
                }
            }

            var task = taskFactory.createViewLoad(files);
            region.tasks.execute(task).continueWith(function (view) {
                if (viewModelPath != null) {
                    var viewModelParameters = {};
                    if (!Bifrost.isNullOrUndefined(view.element)) {
                        viewModelParameters = documentService.getViewModelParametersFrom(element);
                    }
                    viewModelLoader.beginCreateInstanceOfViewModel(viewModelPath, region, viewModelParameters).continueWith(function (viewModelInstance) {
                        region.viewModel = viewModelInstance;
                        promise.signal(view);
                    });
                } else {
                    promise.signal(view);
                }
            });

            return promise;
        };
    })
});