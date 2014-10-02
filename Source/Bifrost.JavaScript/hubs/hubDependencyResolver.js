Bifrost.dependencyResolvers.hub = {
    canResolve: function (namespace, name) {
        if (typeof hubs !== "undefined") {
            return name in hubs;
        }
        return false;
    },

    resolve: function (namespace, name) {
        return hubs[name].create();
    }
};