Bifrost.namespace("Bifrost.views", {
    regionManager: Bifrost.Singleton(function (documentService) {
        /// <summary>Represents a manager that knows how to deal with Regions on the page</summary>
        var self = this;

        function describeRegion(view, region) {
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
        }

        function manageInheritance(element) {
            var parentRegion = documentService.getParentRegionFor(element);
            if (parentRegion) {
                Bifrost.views.Region.prototype = parentRegion;
            } else {
                Bifrost.views.Region.prototype = {};
            }
            return parentRegion;
        }

        function manageHierarchy(parentRegion, view) {
            var region = new Bifrost.views.Region();
            region.parent = parentRegion;
            region.view = view;
            if (parentRegion) {
                parentRegion.children.push(region);
            }
            return region;
        }

        this.getFor = function (view) {
            /// <summary>Gets the region for the given element and creates one if none exist</summary>
            /// <param name="element" type="HTMLElement">Element to get a region for</param>
            /// <returns>The region for the element</returns>
            var promise = Bifrost.execution.Promise.create();

            var element = view.element;

            if (documentService.hasOwnRegion(element)) {
                promise.signal(documentService.getRegionFor(element));
                return promise;
            }

            var parentRegion = manageInheritance(element);
            var region = manageHierarchy(parentRegion, view);

            describeRegion(view, region).continueWith(function () {
                documentService.setRegionOn(element, region);
                promise.signal(region);
            });

            return promise;
        };

        this.evict = function (region) {
            /// <summary>Evict a region from the page</summary>
            /// <param name="region" type="Bifrost.views.Region">Region to evict</param>

            if (region.parentRegion) {
                region.parentRegion.children.remove(region);
            }
            region.parentRegion = null;
        };
    })
});