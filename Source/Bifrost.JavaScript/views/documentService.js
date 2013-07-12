Bifrost.namespace("Bifrost.views", {
    documentService: Bifrost.Singleton(function (DOMRoot) {
        var self = this;

        this.DOMRoot = DOMRoot;

        this.getAllElementsWithViewModelFiles = function () {
            var elements = [];
            $("[data-viewmodel-file]", self.DOMRoot).each(function () {
                elements.push(this);
            });
            return elements;
        };

        this.getAllElementsWithViewModelFilesFrom = function (root) {
            var elements = [];
            $(root).contents().andSelf().filter("[data-viewmodel-file]").each(function () {
                elements.push(this);
            });
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
    })
});