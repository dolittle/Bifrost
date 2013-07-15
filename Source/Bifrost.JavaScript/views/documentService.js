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
    })
});