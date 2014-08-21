Bifrost.namespace("Bifrost.views", {
    viewModelLoader: Bifrost.Singleton(function (taskFactory, fileFactory, viewModelTypes) {
        this.load = function (path, region, viewModelParameters) {
            var promise = Bifrost.execution.Promise.create();
            var file = fileFactory.create(path, Bifrost.io.fileType.javaScript);
            var task = taskFactory.createViewModelLoad([file]);
            region.tasks.execute(task).continueWith(function () {
                viewModelTypes.beginCreateInstanceOfViewModel(path, region, viewModelParameters).continueWith(function (instance) {
                    promise.signal(instance);
                });
            });
            return promise;
        };
    })
});
