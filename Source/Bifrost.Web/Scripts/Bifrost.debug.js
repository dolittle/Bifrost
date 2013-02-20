var Bifrost = Bifrost || {};
(function(global, undefined) {
	Bifrost.extend = function extend(destination, source) {
    	return $.extend(destination, source);
	};
})(window);
var Bifrost = Bifrost || {};
Bifrost.namespace = function (ns, content) {
    var parent = window;
    var name = "";
    var parts = ns.split('.');
    $.each(parts, function (index, part) {
        if (name.length > 0) {
            name += ".";
        }
        name += part;
        if (!Object.prototype.hasOwnProperty.call(parent, part)) {
            parent[part] = {};
            parent[part].parent = parent;
            parent[part].name = name;
        }
        parent = parent[part];
    });

    if (typeof content === "object") {
        Bifrost.namespace.current = parent;

        Bifrost.extend(parent, content);

        for (var property in parent) {
            if (parent.hasOwnProperty(property)) {
                parent[property]._namespace = parent;
                parent[property]._name = property;
            }
        }
        Bifrost.namespace.current = null;
    }

    return parent;
};
Bifrost.namespace("Bifrost", {
    namespaces: (function () {
        var self = this;
        this.conventions = [];

        this.addConvention = function (path, namespace) {
            path = self.stripPath(path);
            self.conventions.push({
                path: path,
                namespace: namespace
            });
        };

        this.stripPath = function (path) {
            if (path.startsWith("/")) {
                path = path.substr(1);
            }
            if (path.endsWith("/")) {
                path = path.substr(0, path.length - 1);
            }
            return path;
        };

        this.initialize = function () {
            var scripts = Bifrost.assetsManager.getScripts();
            if (typeof scripts === "undefined") return;

            $.each(scripts, function (index, fullPath) {
                var path = Bifrost.path.getPathWithoutFilename(fullPath);
                path = self.stripPath(path);
                $.each(self.conventions, function (conventionIndex, convention) {
                    if (path.startsWith(convention.path)) {
                        var namespacePath = path.substr(convention.path.length);
                        namespacePath = self.stripPath(namespacePath);
                        namespacePath = namespacePath.split("/").join(".");
                        if (convention.namespace.length > 0) {
                            namespacePath = convention.namespace + ((namespacePath.length > 0) ? "."+namespacePath:"");
                        }
                        var namespace = Bifrost.namespace(namespacePath);
                        var root = "/" + path + "/";
                        namespace._path = root;

                        if (typeof namespace._scripts === "undefined") {
                            namespace._scripts = [];
                        }

                        var fileIndex = fullPath.lastIndexOf("/");
                        var file = fullPath.substr(fileIndex + 1);
                        var extensionIndex = file.lastIndexOf(".");
                        var system = file.substr(0, extensionIndex);

                        namespace._scripts.push(system);
                    }
                });
            });
        };

        return {
            addConvention : addConvention,
            initialize: initialize
        };
    })()
});
Bifrost.namespace("Bifrost.execution", {
    Promise: function () {
        var self = this;

        this.signalled = false;
        this.callback = null;
        this.error = null;
        this.hasFailed = false;
        this.failedCallback = null;

        function onSignal() {
            if (self.callback != null && typeof self.callback !== "undefined") {
                if (typeof self.signalParameter !== "undefined") {
                    self.callback(self.signalParameter, Bifrost.execution.Promise.create());
                } else {
                    self.callback(Bifrost.execution.Promise.create());
                }
            }
        }

        this.fail = function (error) {
            if (self.failedCallback != null) self.failedCallback(error);
            self.hasFailed = true;
            self.error = error;
            throw error;
        };

        this.onFail = function (callback) {
            if (self.hasFailed) {
                callback(self.error);
            } else {
                self.failedCallback = callback;
            }
        };


        this.signal = function (parameter) {
            self.signalled = true;
            self.signalParameter = parameter;
            onSignal();
        };

        this.continueWith = function (callback) {
            var nextPromise = Bifrost.execution.Promise.create();
            this.callback = callback;
            if (self.signalled === true) onSignal();
            return nextPromise;
        };
    }
});

Bifrost.execution.Promise.create = function() {
	var promise = new Bifrost.execution.Promise();
	return promise;
};
Bifrost.namespace("Bifrost", {
    isNumber: function (number) {
        return !isNaN(parseFloat(number)) && isFinite(number);
    }
});
Bifrost.namespace("Bifrost", {
	isArray : function(o) {
		return Object.prototype.toString.call(o) === '[object Array]';
	}
});
Bifrost.namespace("Bifrost", {
    path: {
        getPathWithoutFilename: function (fullPath) {
            var lastIndex = fullPath.lastIndexOf("/");
            return fullPath.substr(0, lastIndex);
        }
    }
});
if ( typeof String.prototype.startsWith != 'function' ) {
	String.prototype.startsWith = function( str ) {
		return str.length > 0 && this.substring( 0, str.length ) === str;
	}
};

if ( typeof String.prototype.endsWith != 'function' ) {
	String.prototype.endsWith = function( str ) {
		return str.length > 0 && this.substring( this.length - str.length, this.length ) === str;
	}
};

String.prototype.replaceAll = function (toReplace, replacement) {
    var result = this.split(toReplace).join(replacement);
    return result;
};

String.prototype.toCamelCase = function () {
    var result = this.charAt(0).toLowerCase() + this.substring(1);
    result = result.replaceAll("-", "");
    return result;
};

String.prototype.toPascalCase = function () {
    var result = this.charAt(0).toUpperCase() + this.substring(1);
    result = result.replaceAll("-", "");
    return result;
};

Bifrost.namespace("Bifrost", {
	functionParser: {
		parse: function(func) {
			var result = [];
			
            var match = func.toString ().match (/function\w*\s*\((.*?)\)/);
			var arguments = match[1].split (/\s*,\s*/);
			$.each(arguments, function(index, item) {
				if( item.trim().length > 0 ) {
					result.push({
						name:item
					});
				}
			});
			
			return result;
		}
	}
});
Bifrost.namespace("Bifrost", {
    assetsManager: {
        initialize: function () {
            var promise = Bifrost.execution.Promise.create();
            $.get("/Bifrost/AssetsManager", { extension: "js" }, function (result) {
                Bifrost.assetsManager.scripts = result;
                Bifrost.namespaces.initialize();
                promise.signal();
            }, "json");
            return promise;
        },
        getScripts: function () {
            return Bifrost.assetsManager.scripts;
        },
        getScriptPaths: function () {
            var paths = [];

            $.each(Bifrost.assetsManager.scripts, function (index, fullPath) {
                var path = Bifrost.path.getPathWithoutFilename(fullPath);
                if (paths.indexOf(path) == -1) {
                    paths.push(path);
                }
            });
            return paths;
        }
    }
});
Bifrost.namespace("Bifrost", {
    dependencyResolver: (function () {
        function resolveImplementation(namespace, name) {
            var resolvers = Bifrost.dependencyResolvers.getAll();
            var resolvedSystem = null;
            $.each(resolvers, function (index, resolver) {
                if (resolvedSystem != null) return;
                var canResolve = resolver.canResolve(namespace, name);
                if (canResolve) {
                    resolvedSystem = resolver.resolve(namespace, name);
                    return;
                }
            });

            return resolvedSystem;
        }

        function handleSystemInstance(system) {
            if (system != null &&
                system._super !== null &&
                typeof system._super !== "undefined" &&
                system._super === Bifrost.Type) {
                return system.create();
            }
            return system;
        }

        function beginHandleSystemInstance(system) {
            var promise = Bifrost.execution.Promise.create();

            if (system != null &&
                system._super !== null &&
                typeof system._super !== "undefined" &&
                system._super === Bifrost.Type) {

                system.beginCreate().continueWith(function (result, next) {
                    promise.signal(result);
                });
            } else {
                promise.signal(system);
            }

            return promise;
        }

        return {
            getDependenciesFor: function (func) {
                var dependencies = [];
                var parameters = Bifrost.functionParser.parse(func);
                for (var i = 0; i < parameters.length; i++) {
                    dependencies.push(parameters[i].name);
                }
                return dependencies;
            },

            resolve: function (namespace, name) {
                var resolvedSystem = resolveImplementation(namespace, name);
                if (typeof resolvedSystem === "undefined" || resolvedSystem === null) {
                    console.log("Unable to resolve '" + name + "'");
                    throw new Bifrost.UnresolvedDependencies();
                }

                if (resolvedSystem instanceof Bifrost.execution.Promise) {
                    console.log("'" + name + "' was resolved as an asynchronous dependency, consider using beginCreate() or make the dependency available prior to calling create");
                    throw new Bifrost.AsynchronousDependenciesDetected();
                }

                return handleSystemInstance(resolvedSystem);
            },

            beginResolve: function (namespace, name) {
                var promise = Bifrost.execution.Promise.create();
                Bifrost.configure.ready(function () {
                    var resolvedSystem = resolveImplementation(namespace, name);
                    if (typeof resolvedSystem === "undefined" || resolvedSystem === null) {
                        promise.fail(new Bifrost.UnresolvedDependencies());
                    }

                    if (resolvedSystem instanceof Bifrost.execution.Promise) {
                        resolvedSystem.continueWith(function (system, innerPromise) {

                            beginHandleSystemInstance(system)
                            .continueWith(function (actualSystem, next) {
                                promise.signal(handleSystemInstance(actualSystem));
                            });
                        });
                    } else {
                        promise.signal(handleSystemInstance(resolvedSystem));
                    }
                });

                return promise;
            }
        }
    })()
});
Bifrost.namespace("Bifrost", {
    dependencyResolvers: (function () {
        return {
            getAll: function () {
                var resolvers = [new Bifrost.WellKnownTypesDependencyResolver(), new Bifrost.DefaultDependencyResolver()];
                for (var property in this) {
                    if (property.indexOf("_") != 0 &&
                        this.hasOwnProperty(property) &&
                        typeof this[property] !== "function") {
                        resolvers.push(this[property]);
                    }
                }
                return resolvers;
            }
        };
    })()
});
Bifrost.namespace("Bifrost", {
    DefaultDependencyResolver: function () {
        var self = this;

        this.doesNamespaceHave = function (namespace, name) {
            return namespace.hasOwnProperty(name);
        };

        this.doesNamespaceHaveScriptReference = function (namespace, name) {
            if (namespace.hasOwnProperty("_scripts") && Bifrost.isArray(namespace._scripts)) {
                for (var i = 0; i < namespace._scripts.length; i++) {
                    var script = namespace._scripts[i];
                    if (script === name) {
                        return true;
                    }
                }
            }
            return false;
        };

        this.getFileName = function (namespace, name) {
            var fileName = "";
            if (typeof namespace._path !== "undefined") {
                fileName += namespace._path;
                if (!fileName.endsWith("/")) {
                    fileName += "/";
                }
            }
            fileName += name;
            if (!fileName.endsWith(".js")) {
                fileName += ".js";
            }
            return fileName;

        };

        this.loadScriptReference = function (namespace, name, promise) {
            var fileName = self.getFileName(namespace, name);
            require([fileName], function (system) {
                if (self.doesNamespaceHave(namespace, name)) {
                    system = namespace[name];
                }
                promise.signal(system);
            });
        };


        this.canResolve = function (namespace, name) {
            var current = namespace;
            while (current != null && current != window) {
                if (self.doesNamespaceHave(current, name)) {
                    return true;
                }
                if (self.doesNamespaceHaveScriptReference(current, name) ) {
                    return true;
                }
                if (current === current.parent) break;
                current = current.parent;
            }

            return false;
        };

        this.resolve = function (namespace, name) {
            var current = namespace;
            while (current != null && current != window) {
                if (self.doesNamespaceHave(current, name)) {
                    return current[name];
                }
                if (self.doesNamespaceHaveScriptReference(current, name) ) {
                    var promise = Bifrost.execution.Promise.create();       
                    self.loadScriptReference(current, name, promise);
                    return promise;
                }
                if (current === current.parent) break;
                current = current.parent;

            }

            return null;
        };
    }
});
Bifrost.namespace("Bifrost", {
    WellKnownTypesDependencyResolver: function () {
        var self = this;
        this.types = Bifrost.WellKnownTypesDependencyResolver.types;

        this.canResolve = function (namespace, name) {
            return self.types.hasOwnProperty(name);
        },
        this.resolve = function (namespace, name) {
            return self.types[name];
        }
    }
});

Bifrost.WellKnownTypesDependencyResolver.types = {
    options: {}
};
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
    };

    throwIfTypeDefinitionIsObjectLiteral = function(typeDefinition) {
        if (typeof typeDefinition === "object") {
            throw new Bifrost.ObjectLiteralNotAllowed();
        }
    };

    addStaticProperties = function(typeDefinition) {
        for (var property in Bifrost.Type) {
            if (Bifrost.Type.hasOwnProperty(property)) {
                typeDefinition[property] = Bifrost.Type[property];
            }
        }
    };

    setupDependencies = function(typeDefinition) {
        typeDefinition._dependencies = Bifrost.dependencyResolver.getDependenciesFor(typeDefinition);

        var firstParameter = true;
        var createFunctionString = "Function('definition', 'dependencies','return new definition(";
            
        if( typeof typeDefinition._dependencies !== "undefined" ) {
            $.each(typeDefinition._dependencies, function(index, dependency) {
                if (!firstParameter) {
                    createFunctionString += ",";
                }
                firstParameter = false;
                createFunctionString += "dependencies[" + index + "]";
            });
        }
        createFunctionString += ");')";

        typeDefinition.createFunction = eval(createFunctionString);
    };

    getDependencyInstances = function(namespace, typeDefinition) {
        var dependencyInstances = [];
        if( typeof typeDefinition._dependencies !== "undefined" ) {
            $.each(typeDefinition._dependencies, function(index, dependency) {
                var dependencyInstance = Bifrost.dependencyResolver.resolve(namespace, dependency);
                dependencyInstances.push(dependencyInstance);
            });
        }
        return dependencyInstances;
    };

    resolve = function(namespace, dependency, index, instances, typeDefinition, resolvedCallback) {
        var resolverPromise = 
            Bifrost.dependencyResolver
                .beginResolve(namespace, dependency)
                .continueWith(function(result, nextPromise) {
                    instances[index] = result;
                    resolvedCallback(result, nextPromise);
                });
    };


    beginGetDependencyInstances = function(namespace, typeDefinition) {
        var promise = Bifrost.execution.Promise.create();
        var dependencyInstances = [];
        var solvedDependencies = 0;
        if( typeof typeDefinition._dependencies !== "undefined" ) {
            var dependenciesToResolve = typeDefinition._dependencies.length;
            var actualDependencyIndex = 0;
            var dependency = "";
            for( var dependencyIndex=0; dependencyIndex<dependenciesToResolve; dependencyIndex++ ) {
                dependency = typeDefinition._dependencies[dependencyIndex];
                resolve(namespace, dependency, dependencyIndex, dependencyInstances, typeDefinition, function(result, nextPromise) {
                    solvedDependencies++;
                    if( solvedDependencies == dependenciesToResolve ) {
                        promise.signal(dependencyInstances);
                    } 
                });
            }

        }
        return promise;
    };

    expandInstancesHashToDependencies = function(typeDefinition, instanceHash, dependencyInstances) {
        if( typeof typeDefinition._dependencies === "undefined" || typeDefinition._dependencies == null ) return;
        for( var dependency in instanceHash ) {
            for( var dependencyIndex=0; dependencyIndex<typeDefinition._dependencies.length; dependencyIndex++ ) {
                if( typeDefinition._dependencies[dependencyIndex] == dependency ) {
                    dependencyInstances[dependencyIndex] = instanceHash[dependency];
                }
            }
        }
    };

    expandDependenciesToInstanceHash = function(typeDefinition, dependencies, instanceHash) {
        for( var dependencyIndex=0; dependencyIndex<dependencies.length; dependencyIndex++ ) {
            instanceHash[typeDefinition._dependencies[dependencyIndex]] = dependencies[dependencyIndex];
        }
    };

    resolveDependencyInstancesThatHasNotBeenResolved = function(dependencyInstances, typeDefinition) {
        $.each(dependencyInstances, function(index, dependencyInstance) {
            if( dependencyInstance == null || typeof dependencyInstance == "undefined" ) {
                var dependency = typeDefinition._dependencies[index];
                dependencyInstances[index] = Bifrost.dependencyResolver.resolve(typeDefinition._namespace, dependency);
            }
        });
    };

    resolveDependencyInstances = function(instanceHash, typeDefinition) {
        var dependencyInstances = [];
        if( typeof instanceHash === "object" ) {
            expandInstancesHashToDependencies(typeDefinition, instanceHash, dependencyInstances);
        } 
        if( typeof typeDefinition._dependencies !== "undefined" && typeDefinition._dependencies.length > 0 ) {
            if( dependencyInstances.length > 0 ) {
                resolveDependencyInstancesThatHasNotBeenResolved(dependencyInstances, typeDefinition);
            } else {
                dependencyInstances = getDependencyInstances(typeDefinition._namespace, typeDefinition);
            }
        }
        return dependencyInstances;
    };

    handleOnCreate = function(type, lastDescendant, currentInstance) {
        if( currentInstance == null || typeof currentInstance === "undefined" ) return;

        if( typeof type !== "undefined" && typeof type.prototype !== "undefined" ) {
            handleOnCreate(type._super, lastDescendant, type.prototype);
        }

        if( currentInstance.hasOwnProperty("onCreated") && typeof currentInstance.onCreated === "function" ) {
            currentInstance.onCreated(lastDescendant);
        }
    };

    Bifrost.Type.scope = {
        getFor : function(namespace, name) {
            return null;
        }
    };

    Bifrost.Type.extend = function (typeDefinition) {
        throwIfMissingTypeDefinition(typeDefinition);
        throwIfTypeDefinitionIsObjectLiteral(typeDefinition);
        addStaticProperties(typeDefinition);
        setupDependencies(typeDefinition);
        typeDefinition._super = this;
        typeDefinition._typeId = Bifrost.Guid.create();
        return typeDefinition;
    };

    Bifrost.Type.scopeTo = function(scope) {
        if( typeof scope === "function" ) {
            this.scope = {
                getFor: scope
            }
        } else {
            if( typeof scope.getFor === "function" ) {
                this.scope = scope;
            } else {
                this.scope = {
                    getFor: function() {
                        return scope;
                    }
                }
            }
        }
        return this;
    };

    Bifrost.Type.create = function (instanceHash, isSuper) {
        var actualType = this;
        if( this._super != null ) {
            actualType.prototype = this._super.create(instanceHash, true);
        }
        var dependencyInstances = resolveDependencyInstances(instanceHash, this);
        var scope = null;
        if( this != Bifrost.Type ) {
            this.instancesPerScope = this.instancesPerScope || {};

            scope = this.scope.getFor(this._namespace, this._name, this._typeId);
            if (scope != null && this.instancesPerScope.hasOwnProperty(scope)) {
                return this.instancesPerScope[scope];
            }
        }

        var instance = null;
        if( typeof this.createFunction !== "undefined" ) {
            instance = this.createFunction(this, dependencyInstances);
        } else {
            instance = new actualType();    
        }

        if( isSuper !== true ) {
            handleOnCreate(actualType, instance, instance);
        }


        instance._type = {
            _name : this._name,
            _namespace : this._namespace
        };

        if( scope != null ) {
            this.instancesPerScope[scope] = instance;
        }

        return instance;
    };

    Bifrost.Type.beginCreate = function(instanceHash) {
        var self = this;

        var promise = Bifrost.execution.Promise.create();
        var superPromise = Bifrost.execution.Promise.create();

        if( this._super != null ) {
            this._super.beginCreate().continueWith(function(_super, nextPromise) {
                superPromise.signal(_super);
            });
        } else {
            superPromise.signal(null);
        }

        superPromise.continueWith(function(_super, nextPromise) {
            self.prototype = _super;

            if( self._dependencies == null || 
                typeof self._dependencies == "undefined" || 
                self._dependencies.length == 0) {

                var instance = self.create();
                promise.signal(instance);
            } else {
                beginGetDependencyInstances(self._namespace, self)
                    .continueWith(function(dependencies, nextPromise) {
                        var dependencyInstances = {};
                        expandDependenciesToInstanceHash(self, dependencies, dependencyInstances);
                        if( typeof instanceHash === "object" ) {
                            for( var property in instanceHash ) {
                                dependencyInstances[property] = instanceHash[property];
                            }
                        }

                        var instance = self.create(dependencyInstances);
                        promise.signal(instance);
                    });

            }
        });

        return promise;
    };
})();
Bifrost.namespace("Bifrost", {
    Singleton: function (typeDefinition) {
        return Bifrost.Type.extend(typeDefinition).scopeTo(window);
    }
});
Bifrost.namespace("Bifrost");

Bifrost.DefinitionMustBeFunction = function(message) {
    this.prototype = Error.prototype;
	this.name = "DefinitionMustBeFunction";
    this.message = message || "Definition must be function";
}

Bifrost.MissingName = function(message) {
	this.prototype = Error.prototype;
	this.name = "MissingName";
	this.message = message || "Missing name";
}

Bifrost.Exception = (function(global, undefined) {
	function throwIfNameMissing(name) {
		if( !name || typeof name == "undefined" ) throw new Bifrost.MissingName();
	}
	
	function throwIfDefinitionNotAFunction(definition) {
		if( typeof definition != "function" ) throw new Bifrost.DefinitionMustBeFunction();
	}

	function getExceptionName(name) {
		var lastDot = name.lastIndexOf(".");
		if( lastDot == -1 && lastDot != name.length ) return name;
		return name.substr(lastDot+1);
	}
	
	function defineAndGetTargetScope(name) {
		var lastDot = name.lastIndexOf(".");
		if( lastDot == -1 ) {
			return global;
		}
		
		var ns = name.substr(0,lastDot);
		Bifrost.namespace(ns);
		
		var scope = global;
        var parts = ns.split('.');
		$.each(parts, function(index, part) {
			scope = scope[part];
		});
		
		return scope;
	}
	
	return {
		define: function(name, defaultMessage, definition) {
			throwIfNameMissing(name);
			
			var scope = defineAndGetTargetScope(name);
			var exceptionName = getExceptionName(name);
			
			var exception = function(message) {
				this.name = exceptionName;
				this.message = message || defaultMessage;
			}
			exception.prototype = Error.prototype;
			
			if( definition && typeof definition != "undefined" ) {
				throwIfDefinitionNotAFunction(definition);
				
				definition.prototype = Error.prototype;
				exception.prototype = new definition();
			}
			
			scope[exceptionName] = exception;
		}
	};
})(window);
Bifrost.namespace("Bifrost");
Bifrost.Exception.define("Bifrost.LocationNotSpecified","Location was not specified");
Bifrost.Exception.define("Bifrost.InvalidUriFormat", "Uri format specified is not valid");
Bifrost.Exception.define("Bifrost.ObjectLiteralNotAllowed", "Object literal is not allowed");
Bifrost.Exception.define("Bifrost.MissingTypeDefinition", "Type definition was not specified");
Bifrost.Exception.define("Bifrost.AsynchronousDependenciesDetected", "You should consider using Type.beginCreate() or dependencyResolver.beginResolve() for systems that has asynchronous dependencies");
Bifrost.Exception.define("Bifrost.UnresolvedDependencies", "Some dependencies was not possible to resolve");
