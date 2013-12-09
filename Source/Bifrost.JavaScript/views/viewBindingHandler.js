Bifrost.namespace("Bifrost.views", {
    viewBindingHandler: Bifrost.Type.extend(function (viewRenderers, pathResolvers, viewFactory, viewModelManager, documentService) {
        var self = this;

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
            /*if (documentService.hasViewFile(element)) {
                return;
            }*/

            var uri = ko.utils.unwrapObservable(valueAccessor());
            documentService.setViewFileOn(element, uri);

            $(element).data("view", uri);
            if (pathResolvers.canResolve(element, uri)) {
                var actualPath = pathResolvers.resolve(element, uri);
                var view = viewFactory.createFrom(actualPath);
                view.element = element;
                view.content = element.innerHTML;
                self.render(element);

                renderChildren(element);
            }
        };

        this.render = function (element) {
            var promise = Bifrost.execution.Promise.create();

            if (viewRenderers.canRender(element)) {
                viewRenderers.render(element).continueWith(function (view) {
                    var newElement = view.element;
                    newElement.view = view;
                    viewModelManager.applyToViewIfAny(view).continueWith(function () {
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
Bifrost.views.viewBindingHandler.initialize = function () {
    ko.bindingHandlers.view = Bifrost.views.viewBindingHandler.create();
};

