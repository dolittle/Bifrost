Bifrost.namespace("Bifrost.views", {
    regionDescriptorManager: Bifrost.Singleton(function () {
        var self = this;

        this.describe = function (view, region) {
            var promise = Bifrost.execution.Promise.create();
            var localPath = Bifrost.path.getPathWithoutFilename(view.path);
            var namespacePath = Bifrost.namespaceMappers.mapPathToNamespace(localPath);
            if (namespacePath != null) {
                var namespace = Bifrost.namespace(namespacePath);

                Bifrost.dependencyResolver.beginResolve(namespace, "RegionDescriptor").continueWith(function (descriptor) {
                    descriptor.describe(region);
                    promise.signal();
                }).onFail(function () {
                    promise.signal();
                });
            } else {
                promise.signal();
            }
            return promise;
        };
    })
});