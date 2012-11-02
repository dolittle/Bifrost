Bifrost.namespace("Bifrost", {
    dependencyResolver: {
        getDependenciesFor: function (func) {
            Bifrost.functionParser.parse(func);
        },

        resolve: function (namespace, name, callback) {
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
    }
});