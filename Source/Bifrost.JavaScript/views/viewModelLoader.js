Bifrost.namespace("Bifrost.views", {
    viewModelLoader: Bifrost.Singleton(function (taskFactory) {
        var self = this;

        this.load = function (path) {
            var promise = Bifrost.execution.Promise.create();
            if (!path.startsWith("/")) path = "/" + path;

            var task = taskFactory.createFileLoad([path]);
            Bifrost.views.Region.current.tasks.execute(task).continueWith(function () {
                self.beginCreateInstanceOfViewModel(path).continueWith(function (instance) {
                    promise.signal(instance);
                });
            });
            return promise;
        };

        this.beginCreateInstanceOfViewModel = function (path) {
            var localPath = Bifrost.path.getPathWithoutFilename(path);
            var filename = Bifrost.path.getFilenameWithoutExtension(path);

            var promise = Bifrost.execution.Promise.create();

            namespacePath = Bifrost.namespaceMappers.mapPathToNamespace(localPath);
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
