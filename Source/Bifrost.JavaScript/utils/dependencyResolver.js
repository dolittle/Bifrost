Bifrost.namespace("Bifrost", {
    dependencyResolver: (function() {
        function resolveImplementation(namespace, name) {
            var resolvers = Bifrost.dependencyResolvers.getAll();
            var resolvedSystem = null;
            $.each(resolvers, function (index, resolver) {
                var canResolve = resolver.canResolve(namespace, name);
                if (canResolve) {
                    resolvedSystem = resolver.resolve(namespace, name);
                    return;
                }
            });

            return resolvedSystem;
        }

        return {
            getDependenciesFor: function (func) {
                Bifrost.functionParser.parse(func);
            },

            resolve: function (namespace, name) {
                var resolvedSystem = resolveImplementation(namespace,name);

                if( resolvedSystem instanceof Bifrost.execution.Promise ) {
                    throw new Bifrost.AsynchronousDependenciesDetected();
                }

                return resolvedSystem;
            },

            beginResolve: function(namespace, name) {
                var promise = Bifrost.execution.Promise.create();
                var resolvedSystem = resolveImplementation(namespace,name);
                if( resolvedSystem instanceof Bifrost.execution.Promise ) {
                    resolvedSystem.continueWith(function(innerPromise, system) {
                        promise.signal(system);
                    });
                } else {
                    promise.signal(resolvedSystem);
                }

                return promise;
            }
        }
    })()
});