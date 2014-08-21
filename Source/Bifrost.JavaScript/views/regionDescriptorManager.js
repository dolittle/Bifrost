Bifrost.namespace("Bifrost.views", {
    regionDescriptorManager: Bifrost.Singleton(function () {
        /// <summary>Represents a manager that knows how to manage region descriptors</summary>

        this.describe = function (view, region) {
            /// <summary>Describe a specific region related to a view</summary>
            /// <param name="view" type="Bifrost.views.View">View related to the region</param>
            /// <param name="region" type="Bifrost.views.Region">Region that needs to be described</param>
            var promise = Bifrost.execution.Promise.create();
            var localPath = Bifrost.Path.getPathWithoutFilename(view.path);
            var namespacePath = Bifrost.namespaceMappers.mapPathToNamespace(localPath);
            if (namespacePath != null) {
                var namespace = Bifrost.namespace(namespacePath);

                Bifrost.views.Region.current = region;
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

        this.describeTopLevel = function (region) {

        };
    })
});