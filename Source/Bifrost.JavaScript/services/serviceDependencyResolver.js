Bifrost.dependencyResolvers.service = {
    canResolve: function (namespace, name) {
        if (typeof services !== "undefined") {
            return name in services;
        }
        return false;
    },

    resolve: function (namespace, name) {
        return services[name].create();
    }
};