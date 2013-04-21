Bifrost.dependencyResolvers.query = {
    canResolve: function (namespace, name) {
        if (typeof queries !== "undefined") {
            return name in queries;
        }
        return false;
    },

    resolve: function (namespace, name) {
        return queries[name].create();
    }
};