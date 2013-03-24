Bifrost.namespace("Bifrost.views", {
    viewManager: Bifrost.Singleton(function (viewRenderers, viewModelManager) {
        var self = this;

        this.viewRenderers = viewRenderers;
        this.viewModelManager = viewModelManager;

        function renderChildren(element) {
            if(element.hasChildNodes() == true) {
                for (var child = element.firstChild; child; child = child.nextSibling) {
                    self.render(child);
                }
            }
        }

        this.initializeLandingPage = function () {
            var body = $("body")[0];
            if (body !== null) {
                var file = Bifrost.path.getFilenameWithoutExtension(document.location.toString());
                if (file == "") file = "index";
                $(body).data("view", file);
                self.render(body);
            }
        };

        this.render = function (element) {
            var promise = Bifrost.execution.Promise.create();

            if (self.viewRenderers.canRender(element)) {
                self.viewRenderers.render(element).continueWith(function (view) {
                    var newElement = view.element;
                    newElement.view = view;
                    self.viewModelManager.applyToViewIfAny(view);
                    renderChildren(newElement);
                });
            } else {
                renderChildren(element);
            }

            return promise;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.viewManager = Bifrost.views.viewManager;