Bifrost.dependencyResolvers.readModelOf = {
    canResolve: function (namespace, name) {
        if (typeof readModels !== "undefined") {
            return name in readModels;
        }
        return false;
    },

    resolve: function (namespace, name) {
        return readModels[name].create();
    }
};