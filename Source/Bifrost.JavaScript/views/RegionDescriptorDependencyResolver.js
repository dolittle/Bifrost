Bifrost.dependencyResolvers.RegionDescriptor = {
    canResolve: function (namespace, name) {
        return name === "RegionDescriptor";
    },

    resolve: function (namespace, name) {
        return {
            describe: function () { }
        };
    }
};