Bifrost.namespace("Bifrost.views", {
    viewModelLoader: Bifrost.Singleton(function () {
        var self = this;

        this.load = function (path) {
            var promise = Bifrost.execution.Promise.create();
            if (!path.startsWith("/")) path = "/" + path;
            require([path], function () {

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

            for (var mapperKey in Bifrost.namespaceMappers) {
                var mapper = Bifrost.namespaceMappers[mapperKey];
                if (typeof mapper.hasMappingFor === "function" && mapper.hasMappingFor(path)) {
                    var namespacePath = mapper.resolve(localPath);
                    var namespace = Bifrost.namespace(namespacePath);

                    if (filename in namespace) {
                        namespace[filename].beginCreate().continueWith(function (instance) {
                            promise.signal(instance);
                        }).onFail(function () {
                            promise.signal({});
                        });
                    }
                }
            }

            return promise;
        };
    })
});
