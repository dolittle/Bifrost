Bifrost.namespace("Bifrost.views", {
    viewManager: Bifrost.Singleton(function (viewResolvers) {
        var self = this;

        this.viewResolvers = viewResolvers;

        function resolveChildren(element) {
            if(element.hasChildNodes() == true) {
                for (var child = element.firstChild; child; child = child.nextSibling) {
                    self.resolve(child);
                }
            }
        }

        this.resolve = function (element) {
            var promise = Bifrost.execution.Promise.create();

            if( self.viewResolvers.canResolve(element) ) {
                var view = self.viewResolvers.resolve(element);
                view.load().continueWith(function() {
                    var newElement = view.element;
                    newElement.view = view;
                    resolveChildren(newElement);
                    element.parentNode.replaceChild(newElement, element);
                    
                });
            } else {
                resolveChildren(element);
            }

            return promise;
        };
    })
});