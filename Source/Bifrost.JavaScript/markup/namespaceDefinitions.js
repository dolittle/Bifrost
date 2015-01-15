Bifrost.namespace("Bifrost.markup", {
    namespaceDefinitions: Bifrost.Singleton(function () {

        this.create = function (prefix) {
            Bifrost.markup.NamespaceDefinition.create({
                prefix: prefix,
            });
        };
    })
});