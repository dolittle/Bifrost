Bifrost.namespace("Bifrost.markup", {
    namespaceDefinitions: Bifrost.Singleton(function () {

        this.create = function (prefix) {
            var definition = Bifrost.markup.NamespaceDefinition.create({
                prefix: prefix,
            });
            return definition;
        };
    })
});