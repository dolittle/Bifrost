Bifrost.namespace("Bifrost.views", {
    viewLoader: Bifrost.Singleton(function (viewModelManager, taskFactory) {
        this.load = function (path) {
            var promise = Bifrost.execution.Promise.create();

            if (!path.startsWith("/")) path = "/" + path;

            var files = [];

            var viewFile = "text!" + path + "!strip";
            if (!Bifrost.path.hasExtension(viewFile)) viewFile = "noext!" + viewFile;
            files.push(viewFile);

            if (viewModelManager.hasForView(path)) {
                var viewModelFile = viewModelManager.getViewModelPathForView(path);
                files.push(viewModelFile);
            }

            var task = taskFactory.createViewLoad(files);
            Bifrost.views.Region.current.tasks.execute(task).continueWith(function (view) {
                promise.signal(view);
            });

            return promise;
        };
    })
});