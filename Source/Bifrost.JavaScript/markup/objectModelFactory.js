Bifrost.namespace("Bifrost.markup", {
    objectModelManager: Bifrost.Singleton(function (dependencyResolver, documentService) {
        
        function tryResolveTargetNamespaces(localName, targets, success, error) {
            function tryResolve(queue) {
                if (queue.length) {
                    var namespace = Bifrost.namespace(targets.shift());

                    var found = false;
                    namespace._scripts.forEach(function (script) {
                        if (script.toLowerCase() === localName.toLowerCase()) {
                            dependencyResolver.beginResolve(namespace, script)
                                .continueWith(function (instance) {
                                    success(instance);
                                })
                                .onFail(function () {
                                    tryResolveTargetNamespaces(localName, targets, success, error);
                                });
                            found = true;
                        }
                    });

                    if (!found) {
                        tryResolveTargetNamespaces(localName, targets, success, error);
                    }

                } else {
                    error();
                }

            }

            tryResolve(targets);
        }


        this.createFrom = function (element, localName, namespaceDefinition, success, error) {
            tryResolveTargetNamespaces(localName, namespaceDefinition.targets, success, error);
        };
    })
});