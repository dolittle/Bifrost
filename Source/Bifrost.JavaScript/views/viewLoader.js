Bifrost.namespace("Bifrost.views", {
    viewLoader: Bifrost.Singleton(function (viewModelManager, taskFactory, fileFactory, regionManager) {
        this.load = function (path) {
            var promise = Bifrost.execution.Promise.create();
            
            var files = [];

            var viewFile = fileFactory.create(path, Bifrost.io.fileType.html);
            files.push(viewFile);

            if (viewModelManager.hasForView(path)) {
                var viewModelPath = viewModelManager.getViewModelPathForView(path);
                if (!viewModelManager.isLoaded(viewModelPath)) {
                    var viewModelFile = fileFactory.create(viewModelPath, Bifrost.io.fileType.javaScript);
                    files.push(viewModelFile);
                }
            }

            var task = taskFactory.createViewLoad(files);
            regionManager.getCurrent().tasks.execute(task).continueWith(function (view) {
                promise.signal(view);
            });

            return promise;
        };
    })
});