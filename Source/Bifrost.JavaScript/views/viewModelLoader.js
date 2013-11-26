Bifrost.namespace("Bifrost.views", {
    viewModelLoader: Bifrost.Singleton(function () {
        var self = this;

        this.load = function (path, region) {
            var promise = Bifrost.execution.Promise.create();
            if (!path.startsWith("/")) path = "/" + path;
            require([path], function () {

                self.beginCreateInstanceOfViewModel(path, region).continueWith(function (instance) {
                    promise.signal(instance);
                });
            });
            return promise;
        };

        this.beginCreateInstanceOfViewModel = function (path, region) {
            var localPath = Bifrost.path.getPathWithoutFilename(path);
            var filename = Bifrost.path.getFilenameWithoutExtension(path);

            var promise = Bifrost.execution.Promise.create();

            namespacePath = Bifrost.namespaceMappers.mapPathToNamespace(localPath);
            if (namespacePath != null) {
                var namespace = Bifrost.namespace(namespacePath);

                if (filename in namespace) {
                    namespace[filename].beginCreate({
                        region: region
                    }).continueWith(function (instance) {
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
