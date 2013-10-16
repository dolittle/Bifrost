Bifrost.namespace("Bifrost", {
    KnownArtifactTypesDependencyResolver: function () {
        var self = this;
        var supportedArtifacts = {
            readModelTypes: Bifrost.read.ReadModelOf,
            commandTypes: Bifrost.commands.Command,
            queryTypes: Bifrost.read.Query
        };

        this.canResolve = function (namespace, name) {
            return name in supportedArtifacts;
        },
        this.resolve = function (namespace, name) {
            var type = supportedArtifacts[name];
            var extenders = type.getExtendersIn(namespace);
            var resolvedTypes = {};

            extenders.forEach(function (extender) {
                var name = extender._name;
                if (resolvedTypes[name] && resolvedTypes[name]._namespace.name.length > extender._namespace.name.length)
                    return;

                resolvedTypes[name] = extender;
            });

            return resolvedTypes;
        }
    }
})