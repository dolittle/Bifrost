Bifrost.dependencyResolvers.Region = {
    canResolve: function (namespace, name) {
        return name === "region";
    },

    resolve: function (namespace, name) {
        return Bifrost.views.Region.current;
    }
};