Bifrost.namespace("Bifrost", {
    Type: function () {
        var self = this;
        this.doStuff = function () {
            print("Doing stuff : "+this.horse);
        }
    }
});

(function () {
    throwIfMissingTypeDefinition = function(typeDefinition) {
        if (typeDefinition == null || typeof typeDefinition == "undefined") {
            throw new Bifrost.MissingTypeDefinition();
        }
    }

    throwIfTypeDefinitionIsObjectLiteral = function(typeDefinition) {
        
        if (typeof typeDefinition === "object") {
            throw new Bifrost.ObjectLiteralNotAllowed();
        }
    }

    addStaticProperties = function(typeDefinition) {
        for (var property in Bifrost.Type) {
            if (Bifrost.Type.hasOwnProperty(property)) {
                typeDefinition[property] = Bifrost.Type[property];
            }
        }
    }

    Bifrost.Type.define = function (typeDefinition) {
        throwIfMissingTypeDefinition(typeDefinition);
        throwIfTypeDefinitionIsObjectLiteral(typeDefinition);
        addStaticProperties(typeDefinition);
        typeDefinition.super = this;
        return typeDefinition;
    };

    Bifrost.Type.create = function () {
        var actualType = this;
        if( this.super != null ) {
            actualType.prototype = this.super.create();
        }
        var instance = new actualType();
        return instance;
    };
})();