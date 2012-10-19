Bifrost.namespace("Bifrost", {
    dependencyResolver: {
        resolve: function (name, callback) {
            for (var resolverName in Bifrost.dependencyResolvers) {
                if (Bifrost.dependencyResolvers.hasOwnProperty(resolverName)) {
                    var resolver = Bifrost.dependencyResolvers[resolverName];
                    var canResolve = resolver.canResolve(name) === true;
                    if (canResolve) {
                        resolver.resolve(callback);
                    }
                }
            }
        }
    }
});