Bifrost.namespace("Bifrost.views", {
    viewModelLoader: Bifrost.Singleton(function (taskFactory, fileFactory) {
        var self = this;

        this.load = function (path) {
            var promise = Bifrost.execution.Promise.create();
            var file = fileFactory.create(path, Bifrost.io.fileType.javaScript);
            var task = taskFactory.createViewModelLoad([file]);
            Bifrost.views.Region.current.tasks.execute(task).continueWith(function () {
                self.beginCreateInstanceOfViewModel(path).continueWith(function (instance) {
                    promise.signal(instance);
                });
            });
            return promise;
        };

        this.beginCreateInstanceOfViewModel = function (path) {
            var localPath = Bifrost.Path.getPathWithoutFilename(path);
            var filename = Bifrost.Path.getFilenameWithoutExtension(path);

            var promise = Bifrost.execution.Promise.create();

            var namespacePath = Bifrost.namespaceMappers.mapPathToNamespace(localPath);
            if (namespacePath != null) {
                var namespace = Bifrost.namespace(namespacePath);

                if (filename in namespace) {
                    namespace[filename]
                        .beginCreate()
                            .continueWith(function (instance) {
                                promise.signal(instance);
                            }).onFail(function () {
                                promise.signal({});
                            });
                }
            }

            return promise;
        };
    })
});
