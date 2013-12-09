Bifrost.namespace("Bifrost.views", {
    viewManager: Bifrost.Singleton(function (viewRenderers, viewFactory, pathResolvers, viewModelManager, regionManager) {
        var self = this;
        
        this.viewRenderers = viewRenderers;
        this.viewFactory = viewFactory;
        this.pathResolvers = pathResolvers;
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
                var file = Bifrost.Path.getFilenameWithoutExtension(document.location.toString());
                if (file == "") file = "index";
                $(body).data("view", file);

                if (self.pathResolvers.canResolve(body, file)) {
                    var actualPath = self.pathResolvers.resolve(body, file);
                    var view = self.viewFactory.createFrom(actualPath);
                    view.element = body;
                    view.content = body.innerHTML;

                    // Todo: this one destroys the bubbling of click event to the body tag..  Weird.. Need to investigate more (see GitHub issue 233 : https://github.com/dolittle/Bifrost/issues/233)
                    //self.viewModelManager.applyToViewIfAny(view);

                    regionManager.getFor(view).continueWith(function (region) {
                        Bifrost.views.Region.current = region;
                        renderChildren(body);
                    });
                }
            }
        };

        this.render = function (element) {
            var promise = Bifrost.execution.Promise.create();

            if (self.viewRenderers.canRender(element)) {
                self.viewRenderers.render(element).continueWith(function (view) {
                    var newElement = view.element;
                    newElement.view = view;
                    self.viewModelManager.applyToViewIfAny(view).continueWith(function () {
                        renderChildren(newElement);
                    });
                });
            } else {
                renderChildren(element);
            }

            return promise;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.viewManager = Bifrost.views.viewManager;