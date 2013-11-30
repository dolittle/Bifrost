Bifrost.namespace("Bifrost.views", {
    regionManager: Bifrost.Singleton(function (documentService, regionDescriptorManager, messengerFactory, operationsFactory, tasksFactory) {
        /// <summary>Represents a manager that knows how to deal with Regions on the page</summary>
        var self = this;

        function createRegionInstance() {
            var instance = new Bifrost.views.Region(messengerFactory, operationsFactory, tasksFactory);
            return instance;
        }


        function manageInheritance(element) {
            var parentRegion = documentService.getParentRegionFor(element);
            if (parentRegion) {
                Bifrost.views.Region.prototype = parentRegion;
            } else {
                var topLevel = createRegionInstance();
                regionDescriptorManager.describeTopLevel(topLevel);
                Bifrost.views.Region.prototype = topLevel;
            }
            return parentRegion;
        }

        function manageHierarchy(parentRegion, view) {
            var region = createRegionInstance();
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

            regionDescriptorManager.describe(view, region).continueWith(function () {
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