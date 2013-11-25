Bifrost.namespace("Bifrost.views", {
    regionManager: Bifrost.Singleton(function (documentService) {
        /// <summary>Represents a manager that knows how to deal with Regions on the page</summary>
        var self = this;

        this.getFor = function (element) {
            /// <summary>Gets the region for the given element and creates one if none exist</summary>
            /// <param name="element" type="HTMLElement">Element to get a region for</param>
            /// <returns>The region for the element</returns>
        };
    })
});