Bifrost.namespace("Bifrost.views", {
    viewManager: Bifrost.Singleton(function (viewFactory, pathResolvers,regionManager, UIManager, taskFactory, viewRenderers, viewModelManager) {
        var self = this;
        
        this.viewRenderers = viewRenderers;
        this.viewModelManager = viewModelManager;

        this.viewFactory = viewFactory;
        this.pathResolvers = pathResolvers;

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
                    body.view = view;

                    // Todo: this one destroys the bubbling of click event to the body tag..  Weird.. Need to investigate more (see GitHub issue 233 : https://github.com/dolittle/Bifrost/issues/233)
                    //self.viewModelManager.applyToViewIfAny(view);

                    regionManager.getFor(view).continueWith(function (region) {
                        Bifrost.views.Region.current = region;
                        renderChildren(body);
                        UIManager.handle(body);
                    });
                }
            }
        };

        this.render = function (element) {
            var promise = Bifrost.execution.Promise.create();

            if (viewRenderers.canRender(element)) {
                var task = taskFactory.createViewRender(element);
                regionManager.getCurrent().tasks.execute(task).continueWith(function () {
                    promise.signal();
                });
            }
            
            return promise;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.viewManager = Bifrost.views.viewManager;