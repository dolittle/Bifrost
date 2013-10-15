Bifrost.namespace("Bifrost.views", {
    viewBindingHandler: Bifrost.Type.extend(function (viewRenderers, pathResolvers, viewFactory, viewModelManager) {
        var self = this;
        this.viewRenderers = viewRenderers;
        this.pathResolvers = pathResolvers;
        this.viewFactory = viewFactory;
        this.viewModelManager = viewModelManager;

        function renderChildren(element) {
            if (element.hasChildNodes() == true) {
                for (var child = element.firstChild; child; child = child.nextSibling) {
                    self.render(child);
                }
            }
        }

        this.init = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
        };
        this.update = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
            var uri = ko.utils.unwrapObservable(valueAccessor());

            $(element).data("view", uri);
            //var file = Bifrost.path.getFilenameWithoutExtension(uri);
            if (self.pathResolvers.canResolve(element, uri)) {
                var actualPath = self.pathResolvers.resolve(element, uri);
                var view = self.viewFactory.createFrom(actualPath);
                view.element = element;
                view.content = element.innerHTML;
                //self.render(element);

                // Todo: this one destroys the bubbling of click event to the body tag..  Weird.. Need to investigate more (see GitHub issue 233 : https://github.com/dolittle/Bifrost/issues/233)
                //  self.viewModelManager.applyToViewIfAny(view);

                renderChildren(element);
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
Bifrost.views.viewBindingHandler.initialize = function () {
    ko.bindingHandlers.view = Bifrost.views.viewBindingHandler.create();
};

