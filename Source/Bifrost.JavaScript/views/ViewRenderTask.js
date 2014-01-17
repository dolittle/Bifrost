Bifrost.namespace("Bifrost.views", {
    ViewRenderTask: Bifrost.views.ComposeTask.extend(function (element, viewRenderers, viewModelManager, UIManager) {
        /// <summary>Represents a task for rendering views asynchronously</summary>

        var self = this;
        function render(element) {
            var promise = Bifrost.execution.Promise.create();
            if (viewRenderers.canRender(element)) {
                viewRenderers.render(element).continueWith(function () {
                    var view = element.view;
                    var newElement = view.element;
                    newElement.view = view;

                    viewModelManager.applyToViewIfAny(view).continueWith(function () {
                        if (element.hasChildNodes() == true) {
                            renderChildren(newElement).continueWith(function () {
                                promise.signal();
                            });
                        } else {
                            promise.signal();
                        }
                        UIManager.handle(newElement);
                    });
                });
            } else {
                renderChildren(element).continueWith(function () {
                    promise.signal();
                });
            }
            return promise;
        }

        function renderChildren(element) {
            var promise = Bifrost.execution.Promise.create();
            if (element.hasChildNodes() == true) {
                for (var child = element.firstChild; child; child = child.nextSibling) {
                    
                    render(child).continueWith(function () {
                        promise.signal();
                    });
                }
            } else {
                promise.signal();
            }
            return promise;
        }

        

        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();

            render(element).continueWith(function () {
                promise.signal();
            });

            return promise;
        }
    })
});