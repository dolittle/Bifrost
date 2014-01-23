Bifrost.namespace("Bifrost.views", {
    viewModelLoader: Bifrost.Singleton(function (taskFactory, fileFactory) {
        var self = this;

        this.load = function (path, region) {
            var promise = Bifrost.execution.Promise.create();
            var file = fileFactory.create(path, Bifrost.io.fileType.javaScript);
            var task = taskFactory.createViewModelLoad([file]);
            region.tasks.execute(task).continueWith(function () {
                self.beginCreateInstanceOfViewModel(path, region).continueWith(function (instance) {
                    promise.signal(instance);
                });
            });
            return promise;
        };

        this.beginCreateInstanceOfViewModel = function (path, region, instanceHash) {
            var localPath = Bifrost.Path.getPathWithoutFilename(path);
            var filename = Bifrost.Path.getFilenameWithoutExtension(path);

            var promise = Bifrost.execution.Promise.create();

            var namespacePath = Bifrost.namespaceMappers.mapPathToNamespace(localPath);
            if (namespacePath != null) {
                var namespace = Bifrost.namespace(namespacePath);

                if (filename in namespace) {
                    var previousRegion = Bifrost.views.Region.current;
                    Bifrost.views.Region.current = region;

                    instanceHash = instanceHash || {};
                    instanceHash.region = region;

                    namespace[filename]
                        .beginCreate(instanceHash)
                            .continueWith(function (instance) {
                                promise.signal(instance);
                            }).onFail(function (error) {
                                console.log("ViewModel '"+filename+"' failed instantiation");
                                throw error;
                            });
                }
            }

            return promise;
        };
    })
});
