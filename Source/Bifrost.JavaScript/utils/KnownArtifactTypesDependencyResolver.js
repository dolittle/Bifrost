Bifrost.namespace("Bifrost", {
    KnownArtifactTypesDependencyResolver: function () {
        var self = this;
        var supportedArtifacts = {
            readModelTypes: Bifrost.read.ReadModelOf,
            commandTypes: Bifrost.commands.Command,
            queryTypes: Bifrost.read.Query
        };

        function isMoreSpecificNamespace(base, compareTo) {
            var isDeeper = false;
            var matchesbase = false;

            var baseParts = base.name.split(".");
            var compareToParts = compareTo.name.split(".");

            if (baseParts.length > compareToParts.length) {
                return false;
            }

            for (var i = 0; i < baseParts.length; i++) {
                if (baseParts[i] !== compareToParts[i]) {
                    return false;
                }
            }
            return true;
        }

        this.canResolve = function (namespace, name) {
            return name in supportedArtifacts;
        },
        this.resolve = function (namespace, name) {
            var type = supportedArtifacts[name];
            var extenders = type.getExtendersIn(namespace);
            var resolvedTypes = {};

            extenders.forEach(function (extender) {
                var name = extender._name;
                if (resolvedTypes[name] && !isMoreSpecificNamespace(resolvedTypes[name]._namespace, extender._namespace)) {
                    return;
                }

                resolvedTypes[name] = extender;
            });

            return resolvedTypes;
        };
    }
});