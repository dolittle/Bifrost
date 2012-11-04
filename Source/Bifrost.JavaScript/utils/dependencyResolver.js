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

        function handleSystemInstance(system) {
            if( system != null &&
                system._super !== null &&
                typeof system._super !== "undefined" &&
                system._super ===  Bifrost.Type ) {
                return system.create();
            } 
            return system;
        }

        function beginHandleSystemInstance(system) {
            var promise = Bifrost.execution.Promise.create();

            if( system != null &&
                system._super !== null &&
                typeof system._super !== "undefined" &&
                system._super ===  Bifrost.Type ) {

                system.beginCreate().continueWith(function(next, result) {
                    promise.signal(result);
                });
            } else {
                promise.signal(system);
            }

            return promise;
        }

        return {
            getDependenciesFor: function (func) {
                var dependencies = [];
                var parameters = Bifrost.functionParser.parse(func);
                for( var i=0; i<parameters.length; i++ ) {
                    dependencies.push(parameters[i].name);
                }
                return dependencies;
            },

            resolve: function (namespace, name) {
                var resolvedSystem = resolveImplementation(namespace,name);

                if( resolvedSystem instanceof Bifrost.execution.Promise ) {
                    throw new Bifrost.AsynchronousDependenciesDetected();
                }

                return handleSystemInstance(resolvedSystem);
            },

            beginResolve: function(namespace, name) {
                var promise = Bifrost.execution.Promise.create();
                var resolvedSystem = resolveImplementation(namespace,name);
                if( resolvedSystem instanceof Bifrost.execution.Promise ) {
                    resolvedSystem.continueWith(function(innerPromise, system) {

                        beginHandleSystemInstance(system)
                            .continueWith(function(next, actualSystem) {
                                promise.signal(actualSystem);
                            });
                    });
                } else {
                    promise.signal(resolvedSystem);
                }

                return promise;
            }
        }
    })()
});