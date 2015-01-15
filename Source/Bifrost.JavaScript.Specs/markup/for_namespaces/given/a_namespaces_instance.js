given("a namespaces instance", function () {
    var context = this;

    this.namespaceDefinitions = {
        create: function (prefix) {
            var definition = {
                prefix: prefix,
                targets: [],
                addTarget: function () { }
            };

            sinon.stub(definition, "addTarget", function (target) {
                definition.targets.push(target);
            });
            return definition;
        }
    };

    this.elementNaming = {
        prefixToReturn:"",
        getNamespacePrefixFor: function (element) {
            return this.prefixToReturn;
        }
    };

    this.namespaces = Bifrost.markup.namespaces.createWithoutScope({
        namespaceDefinitions: context.namespaceDefinitions,
        elementNaming: context.elementNaming
    });
});