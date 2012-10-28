Bifrost.namespace("Bifrost", {
    Type: function () {

    }
});

Bifrost.Type.define = function (typeDefinition) {
    if (typeDefinition == null || typeof typeDefinition == "undefined") {
        throw new Bifrost.MissingTypeDefinition();
    }
    if (typeof typeDefinition === "object") {
        throw new Bifrost.ObjectLiteralNotAllowed();
    }
    typeDefinition.prototype = new Bifrost.Type();
    typeDefinition.create = function () {
        return Bifrost.Type.create(typeDefinition);
    };
    return typeDefinition;
};
Bifrost.Type.create = function (typeDefinition) {
}