Bifrost.namespace("Bifrost.markup", {
    NamespaceDefinition: Bifrost.Type.extend(function (prefix) {
        var self = this;
        this.prefix = prefix;

        this.targets = [];

        this.addTarget = function (target) {
            self.targets.push(target);
        };
    })
});