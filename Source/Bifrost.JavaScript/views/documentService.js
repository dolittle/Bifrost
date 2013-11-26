Bifrost.namespace("Bifrost.views", {
    documentService: Bifrost.Singleton(function (DOMRoot) {
        var self = this;

        this.DOMRoot = DOMRoot;

        this.getAllElementsWithViewModelFiles = function () {
            return self.getAllElementsWithViewModelFilesFrom(self.DOMRoot);
        };

        this.getAllElementsWithViewModelFilesFrom = function (root) {
            var elements = [];
            if (typeof $(root).data("viewmodel-file") != "undefined") {
                elements.push(root);
            }
            $("[data-viewmodel-file]",root).each(function () {
                elements.push(this);
            });
            return elements;
        };

        function collectViewModelFilesFrom(parent, elements) {

            if (typeof parent.childNodes != "undefined") {
                parent.childNodes.forEach(function (child) {
                    collectViewModelFilesFrom(child, elements);
                });
            }

            var viewModelFile = $(parent).data("viewmodel-file");
            if (typeof viewModelFile != "undefined") {
                elements.push(parent);
            }
        }

        this.getAllElementsWithViewModelFilesSorted = function () {
            return self.getAllElementsWithViewModelFilesSortedFrom(self.DOMRoot);
        };

        this.getAllElementsWithViewModelFilesSortedFrom = function (root) {
            var elements = [];
            collectViewModelFilesFrom(root, elements);
            return elements;
        };

        this.getViewModelFileFrom = function (element) {
            var file = $(element).data("viewmodel-file");
            if (typeof file == "undefined") file = "";
            return file;
        };

        this.setViewModelFileOn = function (element, file) {
            $(element).data("viewmodel-file", file);
            $(element).attr("data-viewmodel-file", file);
        };

        this.setViewModelOn = function (element, viewModel) {
            element.viewModel = viewModel;
            $(element).data("viewmodel", viewModel);
        };

        this.getViewModelFrom = function (element) {
            return element.viewModel;
        };


        this.hasOwnRegion = function (element) {
            /// <summary>Check if element has its own region</summary>
            /// <param name="element" type="HTMLElement">HTML Element to check</param>
            /// <returns>true if it has its own region, false it not</returns>

            if (element.region) return true;
            return false;
        };

        this.getParentRegionFor = function (element) {
            /// <summary>Get the parent region for a given element</summary>
            /// <param name="element" type="HTMLElement">HTML Element to get for</param>
            /// <returns>An instance of the region, if no region is found it will return null</returns>
            var found = null;

            while (element.parentNode) {
                element = element.parentNode;
                if (element.region) return element.region;
            }

            return found;
        }

        this.getRegionFor = function (element) {
            /// <summary>Get region for an element, either directly or implicitly through the nearest parent, null if none</summary>
            /// <param name="element" type="HTMLElement">HTML Element to get for</param>
            /// <returns>An instance of the region, if no region is found it will return null</returns>
            var found = null;

            if (element.region) return element.region;
            found = self.getParentRegionFor(element);
            return found;
        };

        this.setRegionOn = function (element, region) {
            /// <summary>Set region on a specific element</summary>
            /// <param name="element" type="HTMLElement">HTML Element to set on</param>
            /// <param name="region" type="Bifrost.views.Region">Region to set on element</param>

            element.region = region;
        };
    })
});