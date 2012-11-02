Bifrost.namespace("Bifrost", {
    Type: function () {
        var self = this;
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

    setupDependencies = function(typeDefinition) {
        typeDefinition.dependencies = Bifrost.dependencyResolver.getDependenciesFor(typeDefinition);

        var firstParameter = true;
        var createFunctionString = "Function('definition', 'dependencies','return new definition(";
            
        if( typeof typeDefinition.dependencies !== "undefined" ) {
            $.each(typeDefinition.dependencies, function(index, dependency) {
                if (!firstParameter) {
                    functionString += ",";
                    createString += ",";
                }
                firstParameter = false;
                createFunctionString += "dependencies[" + index + "]";
            });
        }
        createFunctionString += ");')";

        typeDefinition.createFunction = eval(createFunctionString);
    }

    getDependencyInstances = function(typeDefinition) {
        var dependencyInstances = [];
        if( typeof typeDefinition.dependencies !== "undefined" ) {
            $.each(typeDefinition.dependencies, function(index, dependency) {
                var dependencyInstance = Bifrost.dependencyResolver.resolve(dependency);
                dependencyInstances.push(dependencyInstance);
            });
        }
        return dependencyInstances;
    }

    Bifrost.Type.define = function (typeDefinition) {
        throwIfMissingTypeDefinition(typeDefinition);
        throwIfTypeDefinitionIsObjectLiteral(typeDefinition);
        addStaticProperties(typeDefinition);
        setupDependencies(typeDefinition);
        typeDefinition._super = this;
        return typeDefinition;
    };

    Bifrost.Type.create = function () {
        var actualType = this;
        if( this._super != null ) {
            actualType.prototype = this._super.create();
        }
        var dependencyInstances = getDependencyInstances(this);
        var instance = null;
        if( typeof this.createFunction !== "undefined" ) {
            instance = this.createFunction(this, dependencyInstances);
        } else {
            instance = new actualType();    
        }

        return instance;
    };
})();