Bifrost.namespace("Bifrost", {
    dependencyResolver: {
        getDependenciesFor: function(func) {
            Bifrost.functionParser.parse(func);
        },

        resolve: function (name, callback) {
            for (var resolverName in Bifrost.dependencyResolvers) {
                if (Bifrost.dependencyResolvers.hasOwnProperty(resolverName)) {
                    var resolver = Bifrost.dependencyResolvers[resolverName];
                    var canResolve = resolver.canResolve(name) === true;
                    if (canResolve) {
                        return resolver.resolve();
                    }
                }
            }

            return null;
        }
    }
});