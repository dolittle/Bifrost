
function polyfillForEach() {
    if (typeof Array.prototype.forEach !== "function") {
        Array.prototype.forEach = function (callback, thisArg) {
            if( typeof thisArg == "undefined" ) thisArg = window;
            for (var i = 0; i < this.length; i++) {
                callback.call(thisArg, this[i], i, this);
            }
        };
    }
}

function polyFillClone() {
    if (typeof Array.prototype.clone !== "function") {
        Array.prototype.clone = function () {
            return this.slice(0);
        }
    }
}

function shallowEquals() {
    if (typeof Array.prototype.shallowEquals !== "function") {
        Array.prototype.shallowEquals = function (other) {
            if (this === other) return true;
            if (this === null || other === null) return false;
            if (this.length != other.length) return false;

            for (var i = 0; i < this.length; i++) {
                if (this[i] !== other[i]) return false;
            }
            return true;
        }
    }
}

(function () {
    polyfillForEach();
    polyFillClone();
    shallowEquals();
})();
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

NodeList.prototype.forEach = Array.prototype.forEach;
NodeList.prototype.length = Array.prototype.length;
HTMLCollection.prototype.forEach = Array.prototype.forEach;
HTMLCollection.prototype.length = Array.prototype.length;
// From: http://www.jonathantneal.com/blog/faking-the-future/
this.Element && (function (ElementPrototype, polyfill) {
    function NodeList() { [polyfill] }
    NodeList.prototype.length = Array.prototype.length;

    ElementPrototype.matchesSelector = ElementPrototype.matchesSelector ||
    ElementPrototype.mozMatchesSelector ||
    ElementPrototype.msMatchesSelector ||
    ElementPrototype.oMatchesSelector ||
    ElementPrototype.webkitMatchesSelector ||
    function matchesSelector(selector) {
        var results = this.parentNode.querySelectorAll(selector);
        var resultsIndex = -1;
 
        while (results[++resultsIndex] && results[resultsIndex] != this) {}
 
        return !!results[resultsIndex];
    };

    ElementPrototype.ancestorQuerySelectorAll = ElementPrototype.ancestorQuerySelectorAll ||
    ElementPrototype.mozAncestorQuerySelectorAll ||
    ElementPrototype.msAncestorQuerySelectorAll ||
    ElementPrototype.oAncestorQuerySelectorAll ||
    ElementPrototype.webkitAncestorQuerySelectorAll ||
    function ancestorQuerySelectorAll(selector) {
        for (var cite = this, newNodeList = new NodeList; cite = cite.parentElement;) {
            if (cite.matchesSelector(selector)) Array.prototype.push.call(newNodeList, cite);
        }
 
        return newNodeList;
    };
 
    ElementPrototype.ancestorQuerySelector = ElementPrototype.ancestorQuerySelector ||
    ElementPrototype.mozAncestorQuerySelector ||
    ElementPrototype.msAncestorQuerySelector ||
    ElementPrototype.oAncestorQuerySelector ||
    ElementPrototype.webkitAncestorQuerySelector ||
    function ancestorQuerySelector(selector) {
        return this.ancestorQuerySelectorAll(selector)[0] || null;
    };
})(Element.prototype);
var Bifrost = Bifrost || {};
(function(global, undefined) {
	Bifrost.extend = function extend(destination, source) {
    	return $.extend(destination, source);
	};
})(window);
var Bifrost = Bifrost || {};
Bifrost.namespace = function (ns, content) {

    // Todo: this should not be needed, it is a symptom of something using it being wrong!!! Se issue #232 on GitHub (http://github.com/dolittle/Bifrost/issues/232)
    ns = ns.replaceAll("..", ".");
    if (ns.endsWith(".")) ns = ns.substr(0, ns.length - 1);
    if (ns.startsWith(".")) ns = ns.substr(1);

    var parent = window;
    var name = "";
    var parts = ns.split('.');
    parts.forEach(function (part) {
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
	isObject : function(o) {
	    return Object.prototype.toString.call(o) === '[object Object]';
	}
});
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
    isNull: function (value) {
        return value === null;
    }
});
Bifrost.namespace("Bifrost", {
    isString: function (value) {
        return typeof value === "string";
        }
});
Bifrost.namespace("Bifrost", {
    isUndefined: function (value) {
        return typeof value === "undefined";
    }
});
Bifrost.namespace("Bifrost", {
    isFunction: function (value) {
        return typeof value === "function";
    }
});
Bifrost.namespace("Bifrost", {
    path: {
        makeRelative: function (fullPath) {
            if (fullPath.indexOf("/") == 0) return fullPath.substr(1);

            return fullPath;
        },
        getPathWithoutFilename: function (fullPath) {
            var lastIndex = fullPath.lastIndexOf("/");
            return fullPath.substr(0, lastIndex);
        },
        getFilename: function (fullPath) {
            var lastIndex = fullPath.lastIndexOf("/");
            return fullPath.substr(lastIndex+1);
        },
        getFilenameWithoutExtension: function (fullPath) {
            var filename = this.getFilename(fullPath);
            var lastIndex = filename.lastIndexOf(".");
            return filename.substr(0,lastIndex);
        },
        hasExtension: function (path) {
            if (path.indexOf("?") > 0) path = path.substr(0, path.indexOf("?"));
            var lastIndex = path.lastIndexOf(".");
            return lastIndex > 0;
        },
        changeExtension: function (fullPath, newExtension) {
            if (fullPath.indexOf("?") > 0) fullPath = fullPath.substr(0, fullPath.indexOf("?"));
            var lastIndex = fullPath.lastIndexOf(".");
            if (lastIndex > 0) {
                return fullPath.substr(0, lastIndex) + "." + newExtension;
            }
            return fullPath + "." + newExtension;
        }
    }
});
Bifrost.namespace("Bifrost", {
	functionParser: {
		parse: function(func) {
			var result = [];
			
			var match = func.toString().match(/function\w*\s*\((.*?)\)/);
			if (match != null) {
			    var arguments = match[1].split(/\s*,\s*/);
			    arguments.forEach(function (item) {
			        if (item.trim().length > 0) {
			            result.push({
			                name: item
			            });
			        }
			    });
			}
			
			return result;
		}
	}
});
Bifrost.namespace("Bifrost", {
    assetsManager: {
        initialize: function () {
            var promise = Bifrost.execution.Promise.create();
            if (typeof Bifrost.assetsManager.scripts === "undefined" ||
                Bifrost.assetsManager.scripts.length == 0) {

                $.get("/Bifrost/AssetsManager", { extension: "js" }, function (result) {
                    Bifrost.assetsManager.scripts = result;
                    Bifrost.namespaces.create().initialize();
                    promise.signal();
                }, "json");
            } else {
                promise.signal();
            }
            return promise;
        },
        initializeFromAssets: function(assets) {
            Bifrost.assetsManager.scripts = assets;
            Bifrost.namespaces.create().initialize();
        },
        getScripts: function () {
            return Bifrost.assetsManager.scripts;
        },
        hasScript: function(script) {
            var found = false;
            Bifrost.assetsManager.scripts.some(function (scriptInSystem) {
                if (scriptInSystem === script) {
                    found = true;
                    return;
                }
            });

            return found;
        },
        getScriptPaths: function () {
            var paths = [];

            Bifrost.assetsManager.scripts.forEach(function (fullPath) {
                var path = Bifrost.path.getPathWithoutFilename(fullPath);
                if (paths.indexOf(path) == -1) {
                    paths.push(path);
                }
            });
            return paths;
        }
    }
});
//Bifrost.WellKnownTypesDependencyResolver.types.assetsManager = Bifrost.commands.commandCoordinator;
Bifrost.namespace("Bifrost", {
    dependencyResolver: (function () {
        function resolveImplementation(namespace, name) {
            var resolvers = Bifrost.dependencyResolvers.getAll();
            var resolvedSystem = null;
            resolvers.forEach(function (resolver) {
                if (resolvedSystem != null) return;
                var canResolve = resolver.canResolve(namespace, name);
                if (canResolve) {
                    resolvedSystem = resolver.resolve(namespace, name);
                    return;
                }
            });

            return resolvedSystem;
        }

        function isType(system) {
            if (system != null &&
                system._super !== null) {

                if (typeof system._super != "undefined" &&
                    system._super === Bifrost.Type) {
                    return true;
                }

                if (isType(system._super) == true) {
                    return true;
                }
            }

            return false;
        }

        function handleSystemInstance(system) {
            if (isType(system)) {
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
                        console.log("Unable to resolve '" + name + "'");
                        promise.fail(new Bifrost.UnresolvedDependencies());
                    }

                    if (resolvedSystem instanceof Bifrost.execution.Promise) {
                        resolvedSystem.continueWith(function (system, innerPromise) {

                            beginHandleSystemInstance(system)
                            .continueWith(function (actualSystem, next) {
                                promise.signal(handleSystemInstance(actualSystem));
                            }).onFail(function(e) { promise.fail(e); });
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
                var resolvers = [
                    new Bifrost.WellKnownTypesDependencyResolver(),
                    new Bifrost.DefaultDependencyResolver(),
                    new Bifrost.KnownArtifactTypesDependencyResolver(),
                    new Bifrost.KnownArtifactInstancesDependencyResolver(),

                ];
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
            fileName = fileName.replaceAll("//", "/");
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
Bifrost.dependencyResolvers.DOMRootDependencyResolver = {
    canResolve: function (namespace, name) {
        return name === "DOMRoot";
    },

    resolve: function (namespace, name) {
        if (document.body != null && typeof document.body != "undefined") {
            return document.body;
        }

        var promise = Bifrost.execution.Promise.create();
        Bifrost.dependencyResolvers.DOMRootDependencyResolver.promises.push(promise);
        return promise;
    }
};

Bifrost.dependencyResolvers.DOMRootDependencyResolver.promises = [];
Bifrost.dependencyResolvers.DOMRootDependencyResolver.documentIsReady = function () {
    Bifrost.dependencyResolvers.DOMRootDependencyResolver.promises.forEach(function (promise) {
        promise.signal(document.body);
    });
};
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

            if (baseParts.length > compareToParts.length)
                return false;

            for (var i = 0; i < baseParts.length; i++) {
                if (baseParts[i] !== compareToParts[i])
                    return false;
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
                if (resolvedTypes[name] && !isMoreSpecificNamespace(resolvedTypes[name]._namespace, extender._namespace))
                    return;

                resolvedTypes[name] = extender;
            });

            return resolvedTypes;
        }
    }
})
Bifrost.namespace("Bifrost", {
    KnownArtifactInstancesDependencyResolver: function () {
        var self = this;
        var supportedArtifacts = {
            readModels: Bifrost.read.ReadModelOf,
            commands: Bifrost.commands.Command,
            queries: Bifrost.read.Query
        };

        function isMoreSpecificNamespace(base, compareTo) {
            var isDeeper = false;
            var matchesbase = false;

            var baseParts = base.name.split(".");
            var compareToParts = compareTo.name.split(".");

            if (baseParts.length > compareToParts.length)
                return false;

            for (var i = 0; i < baseParts.length; i++) {
                if (baseParts[i] !== compareToParts[i])
                    return false;
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
                if (resolvedTypes[name] && !isMoreSpecificNamespace(resolvedTypes[name]._namespace, extender._namespace))
                    return;

                resolvedTypes[name] = extender;
            });

            var resolvedInstances = {};
            for (var prop in resolvedTypes) {
                resolvedInstances[prop] = resolvedTypes[prop].create();
            }

            return resolvedInstances;
        }
    }
})
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

    addStaticProperties = function (typeDefinition) {
        for (var property in Bifrost.Type) {
            if (Bifrost.Type.hasOwnProperty(property) && property != "_extenders") {
                typeDefinition[property] = Bifrost.Type[property];
            }
        }
    };

    setupDependencies = function(typeDefinition) {
        typeDefinition._dependencies = Bifrost.dependencyResolver.getDependenciesFor(typeDefinition);

        var firstParameter = true;
        var createFunctionString = "Function('definition', 'dependencies','return new definition(";
            
        if( typeof typeDefinition._dependencies !== "undefined" ) {
            typeDefinition._dependencies.forEach(function(dependency, index) {
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
            typeDefinition._dependencies.forEach(function(dependency) {
                var dependencyInstance = Bifrost.dependencyResolver.resolve(namespace, dependency);
                dependencyInstances.push(dependencyInstance);
            });
        }
        return dependencyInstances;
    };

    resolve = function(namespace, dependency, index, instances, typeDefinition, resolvedCallback) {
        var promise = 
            Bifrost.dependencyResolver
                .beginResolve(namespace, dependency)
                .continueWith(function(result, nextPromise) {
                    instances[index] = result;
                    resolvedCallback(result, nextPromise);
                });
        return promise;
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
                }).onFail(function(e) { promise.fail(e); });
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
        dependencyInstances.forEach(function(dependencyInstance, index) {
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

    addMissingDependenciesAsNullFromTypeDefinition = function (instanceHash, typeDefinition) {
        if (typeof typeDefinition._dependencies === "undefined") return;
        if (typeof instanceHash === "undefined" || instanceHash == null) return 
        for( var index=0; index<typeDefinition._dependencies.length; index++ ) {
            var dependency = typeDefinition._dependencies[index];
            if (!(dependency in instanceHash)) {
                instanceHash[dependency] = null;
            }
        }
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

    Bifrost.Type._extenders = [];

    Bifrost.Type.scope = {
        getFor : function(namespace, name) {
            return null;
        }
    };

    Bifrost.Type.typeOf = function (type) {

        if (typeof this._super == "undefined" ||
            typeof this._super._typeId == "undefined") {
            return false;
        }

        if (this._super._typeId === type._typeId) {
            return true;
        }

        if (typeof type._super !== "undefined") {
            var isType = this._super.typeOf(type);
            if (isType == true) return true;
        }


        return false;
    };

    Bifrost.Type.getExtenders = function () {
        return this._extenders;
    };

    Bifrost.Type.getExtendersIn = function (namespace) {
        var inNamespace = [];
        
        this._extenders.forEach(function (extender) {
            var current = namespace;
            while (current !== window) {
                if (extender._namespace == current) {
                    inNamespace.push(extender);
                    break;
                }

                if (Bifrost.isUndefined(current.parent))
                    break;

                current = current.parent;
            }
            
        });
        return inNamespace;
    };

  

    Bifrost.Type.extend = function (typeDefinition) {     
        throwIfMissingTypeDefinition(typeDefinition);
        throwIfTypeDefinitionIsObjectLiteral(typeDefinition);

        addStaticProperties(typeDefinition);
        setupDependencies(typeDefinition);
        typeDefinition._super = this;
        typeDefinition._typeId = Bifrost.Guid.create();
        typeDefinition._extenders = [];
        Bifrost.Type.registerExtender(this, typeDefinition);
        return typeDefinition;
    };

    Bifrost.Type.registerExtender = function (typeExtended, typeDefined) {
        var superType = typeExtended;
        
        while (superType != null) {
            if (superType._extenders.indexOf(typeDefined) === -1) {
                superType._extenders.push(typeDefined);
            }
            superType = superType._super;
        }
    }

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

    Bifrost.Type.defaultScope = function() {
        this.scope = {
            getFor: function() {
                return null;
            }
        };
        return this;
    };

    Bifrost.Type.requires = function () {
        for (var argumentIndex = 0; argumentIndex < arguments.length; argumentIndex++) {
            this._dependencies.push(arguments[argumentIndex]);
        }

        return this;
    };

    Bifrost.Type.create = function (instanceHash, isSuper) {
        var actualType = this;
        if( this._super != null ) {
            actualType.prototype = this._super.create(instanceHash, true);
        }
        addMissingDependenciesAsNullFromTypeDefinition(instanceHash, this);
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

    Bifrost.Type.createWithoutScope = function (instanceHash, isSuper) {
        var scope = this.scope;
        this.defaultScope();
        var instance = this.create(instanceHash, isSuper);
        this.scope = scope;
        return instance;
    };

    Bifrost.Type.beginCreate = function(instanceHash) {
        var self = this;

        var promise = Bifrost.execution.Promise.create();
        var superPromise = Bifrost.execution.Promise.create();
        superPromise.onFail(function (e) {
            promise.fail(e);
        });

        if( this._super != null ) {
            this._super.beginCreate().continueWith(function (_super, nextPromise) {
                superPromise.signal(_super);
            }).onFail(function (e) {
                promise.fail(e);
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

                        try {
                            var instance = self.create(dependencyInstances);
                            promise.signal(instance);
                        } catch (e) {
                            promise.fail(e);
                        }
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
		parts.forEach(function(part) {
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
Bifrost.namespace("Bifrost", {
	Guid : {
       	create: function() {
	    	function S4() {
	        	return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
	    	}
           	return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
       	},
    	empty: "00000000-0000-0000-0000-000000000000"
	}
});
Bifrost.namespace("Bifrost");
Bifrost.hashString = (function() {
	return {
		decode: function(a) {
		    if (a == "") return { };
			a = a.replace("/?","").split('&');

		    var b = { };
		    for (var i = 0; i < a.length; ++i) {
		        var p = a[i].split('=', 2);
		        if (p.length != 2) continue;
		
				var value = decodeURIComponent(p[1].replace( /\+/g , " "));

				if( value !== "" && !isNaN(value) ) {
					value = parseFloat(value);
				}

		        b[p[0]] = value;
		    }
		    return b;
		}
	}
})();
Bifrost.namespace("Bifrost");
Bifrost.Uri = (function(window, undefined) {
	/* parseUri JS v0.1, by Steven Levithan (http://badassery.blogspot.com)
	Splits any well-formed URI into the following parts (all are optional):
	----------------------
	• source (since the exec() method returns backreference 0 [i.e., the entire match] as key 0, we might as well use it)
	• protocol (scheme)
	• authority (includes both the domain and port)
	    • domain (part of the authority; can be an IP address)
	    • port (part of the authority)
	• path (includes both the directory path and filename)
	    • directoryPath (part of the path; supports directories with periods, and without a trailing backslash)
	    • fileName (part of the path)
	• query (does not include the leading question mark)
	• anchor (fragment)
	*/
	function parseUri(sourceUri){
	    var uriPartNames = ["source","protocol","authority","domain","port","path","directoryPath","fileName","query","anchor"];
	    var uriParts = new RegExp("^(?:([^:/?#.]+):)?(?://)?(([^:/?#]*)(?::(\\d*))?)?((/(?:[^?#](?![^?#/]*\\.[^?#/.]+(?:[\\?#]|$)))*/?)?([^?#/]*))?(?:\\?([^#]*))?(?:#(.*))?").exec(sourceUri);
	    var uri = {};

	    for(var i = 0; i < 10; i++){
	        uri[uriPartNames[i]] = (uriParts[i] ? uriParts[i] : "");
	    }

	    // Always end directoryPath with a trailing backslash if a path was present in the source URI
	    // Note that a trailing backslash is NOT automatically inserted within or appended to the "path" key
	    if(uri.directoryPath.length > 0){
	        uri.directoryPath = uri.directoryPath.replace(/\/?$/, "/");
	    }

	    return uri;
	}	
	
	
	function Uri(location) {
		var self = this;
		this.setLocation = function(location) {
			self.fullPath = location;
			location = location.replace("#","/");
		
			var result = parseUri(location);
		
			if( !result.protocol || typeof result.protocol == "undefined" ) {
				throw new Bifrost.InvalidUriFormat("Uri ('"+location+"') was in the wrong format");
			}

			self.scheme = result.protocol;
			self.host = result.domain;
			self.path = result.path;
			self.anchor = result.anchor;

			self.queryString = result.query;
			self.port = parseInt(result.port);
			self.parameters = Bifrost.hashString.decode(result.query);
			
			self.isSameAsOrigin = (window.location.protocol == result.protocol+":" &&
				window.location.hostname == self.host); 
		}
		
		this.setLocation(location);
	}
	
	function throwIfLocationNotSpecified(location) {
		if( !location || typeof location == "undefined" ) throw new Bifrost.LocationNotSpecified();
	}
	
	
	return {
		create: function(location) {
			throwIfLocationNotSpecified(location);
		
			var uri = new Uri(location);
			return uri;
		},
	};
})(window);
Bifrost.namespace("Bifrost", {
    namespaces: Bifrost.Singleton(function() {
        var self = this;

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

            scripts.forEach(function (fullPath) {
                var path = Bifrost.path.getPathWithoutFilename(fullPath);
                path = self.stripPath(path);

                for (var mapperKey in Bifrost.namespaceMappers) {
                    var mapper = Bifrost.namespaceMappers[mapperKey];
                    if (typeof mapper.hasMappingFor === "function" && mapper.hasMappingFor(path)) {
                        var namespacePath = mapper.resolve(path);
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
                }
            });
        };
    })
});
Bifrost.namespace("Bifrost", {
    namespaceMappers: {}
});
Bifrost.namespace("Bifrost", {
    StringMapping: Bifrost.Type.extend(function (format, mappedFormat) {
        var self = this;

        this.format = format;
        this.mappedFormat = mappedFormat;

        var placeholderExpression = "\{[a-zA-Z]+\}";
        var placeholderRegex = new RegExp(placeholderExpression, "g");

        var wildcardExpression = "\\*{2}[//||\.]";
        var wildcardRegex = new RegExp(wildcardExpression, "g");

        var combinedExpression = "(" + placeholderExpression + ")*(" + wildcardExpression + ")*";
        var combinedRegex = new RegExp(combinedExpression, "g");

        var components = [];
        

        var resolveExpression = format.replace(combinedRegex, function(match) {
            if( typeof match === "undefined" || match == "") return "";
            components.push(match);
            if( match.indexOf("**") == 0) return "([\\w.//]*)";
            return "([\\w.]*)";
        });

        var mappedFormatWildcardMatch = mappedFormat.match(wildcardRegex);
        var formatRegex = new RegExp(resolveExpression);

        this.matches = function (input) {
            var match = input.match(formatRegex);
            if (match) {
                return true;
            }
            return false;
        };

        this.getValues = function (input) {
            var output = {};
            var match = input.match(formatRegex);
            components.forEach(function (c, i) {
                var component = c.substr(1, c.length - 2);
                var value = match[i + 2];
                if (c.indexOf("**") != 0) {
                    output[component] = value;
                }
            });

            return output;
        };

        this.resolve = function (input) {
            var match = input.match(formatRegex);
            var result = mappedFormat;
            var wildcardOffset = 0;

            components.forEach(function (c, i) {
                var value = match[i + 1];
                if (c.indexOf("**") == 0) {
                    var wildcard = mappedFormatWildcardMatch[wildcardOffset];
                    value = value.replaceAll(c[2], wildcard[2]);
                    result = result.replace(wildcard, value);
                    wildcardOffset++;
                } else {
                    result = result.replace(c, value);
                }
            });

            return result;
        };
    })
});
Bifrost.namespace("Bifrost", {
    stringMappingFactory: Bifrost.Singleton(function () {
        var self = this;

        this.create = function (format, mappedFormat) {
            var mapping = Bifrost.StringMapping.create({
                format: format,
                mappedFormat: mappedFormat
            });
            return mapping;
        };
    })
});
Bifrost.namespace("Bifrost", {
    StringMapper: Bifrost.Type.extend(function (stringMappingFactory) {
        var self = this;

        this.stringMappingFactory = stringMappingFactory;

        this.mappings = [];

        this.hasMappingFor = function (input) {
            var found = false;
            self.mappings.some(function (m) {
                if (m.matches(input)) {
                    found = true;
                }
                return found;
            });
            return found;
        };

        this.getMappingFor = function (input) {
            var found;
            self.mappings.some(function (m) {
                if (m.matches(input)) {
                    found = m;
                    return true;
                }
            });

            if (typeof found !== "undefined") {
                return found;
            }

            throw {
                name: "ArgumentError",
                message: "String mapping for (" + input + ") could not be found"
            }
        };

        this.resolve = function (input) {
            try {
                if( input === null || typeof input === "undefined" || input == "" ) return "";
                
                var mapping = self.getMappingFor(input);
                return mapping.resolve(input);
            } catch (e) {
                return "";
            }
        };

        this.addMapping = function (format, mappedFormat) {
            var mapping = self.stringMappingFactory.create(format, mappedFormat);
            self.mappings.push(mapping);
        };
    })
});
Bifrost.namespace("Bifrost", {
    uriMappers: {
    }
});
Bifrost.namespace("Bifrost", {
    server: Bifrost.Singleton(function () {
        var self = this;

        function deserialize(data) {
            if (Bifrost.isArray(data)) {
                var items = [];
                data.forEach(function (item) {
                    items.push(deserialize(item));
                });
                return items;
            } else {
                for (var property in data) {
                    if (Bifrost.isArray(data[property])) {
                        data[property] = deserialize(data[property]);
                    } else {
                        var value = data[property];

                        if (Bifrost.isNumber(value)) {
                            data[property] = parseFloat(value);
                        } else {
                            data[property] = data[property];
                        }
                    }
                }
                return data;
            }
        }

        this.post = function (url, parameters) {
            var promise = Bifrost.execution.Promise.create();

            var actualParameters = {};

            for (var property in parameters) {
                actualParameters[property] = JSON.stringify(parameters[property]);
            }

            $.ajax({
                url: url,
                type: "POST",
                dataType: 'json',
                data: JSON.stringify(actualParameters),
                contentType: 'application/json; charset=utf-8',
                complete: function (result) {
                    var data = $.parseJSON(result.responseText);
                    deserialize(data);
                    promise.signal(data);
                }
            });

            return promise;
        };

        this.get = function (url, parameters) {
            var promise = Bifrost.execution.Promise.create();

            $.ajax({
                url: url,
                type: "GET",
                dataType: 'json',
                data: parameters,
                contentType: 'application/json; charset=utf-8',
                complete: function (result) {
                    var data = $.parseJSON(result.responseText);
                    deserialize(data);
                    promise.signal(data);
                }
            });

            return promise;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.server = Bifrost.server;
Bifrost.namespace("Bifrost.validation");
Bifrost.Exception.define("Bifrost.validation.OptionsNotDefined", "Option was undefined");
Bifrost.Exception.define("Bifrost.validation.OptionsValueNotSpecified", "Required value in Options is not specified. ");
Bifrost.Exception.define("Bifrost.validation.NotANumber", "Value is not a number");
Bifrost.Exception.define("Bifrost.validation.NotAString", "Value is not a string");
Bifrost.Exception.define("Bifrost.validation.ValueNotSpecified","Value is not specified");
Bifrost.Exception.define("Bifrost.validation.MinNotSpecified","Min is not specified");
Bifrost.Exception.define("Bifrost.validation.MaxNotSpecified","Max is not specified");
Bifrost.Exception.define("Bifrost.validation.MinLengthNotSpecified","Min length is not specified");
Bifrost.Exception.define("Bifrost.validation.MaxLengthNotSpecified","Max length is not specified");
Bifrost.Exception.define("Bifrost.validation.MissingExpression","Expression is not specified");
Bifrost.namespace("Bifrost.validation");
Bifrost.validation.ruleHandlers = (function () {
    return Bifrost.validation.ruleHandlers || { };
})();
Bifrost.namespace("Bifrost.validation", {
    Rule: Bifrost.Type.extend(function (options) {
        var self = this;
        options = options || {};
        this.message = options.message || "";
        this.options = {};
        Bifrost.extend(this.options, options);

        this.validate = function (value) {
            return true;
        };
    })
});
Bifrost.namespace("Bifrost.validation");
Bifrost.validation.Validator = (function () {
    function Validator(options) {
        var self = this;
        this.isValid = ko.observable(true);
        this.message = ko.observable("");
        this.rules = [];
        options = options || {};

        this.setOptions = function (options) {
            for (var property in options) {
                var ruleTypes = Bifrost.validation.Rule.getExtenders();
                ruleTypes.some(function (ruleType) {
                    if (ruleType._name === property) {
                        var rule = ruleType.create({ options: options[property] || {} });
                        self.rules.push(rule);
                    }
                });
            }
        };

        this.validate = function(value) {
            self.rules.some(function(rule) {
                if (!rule.validate(value)) {
                    self.isValid(false);
                    self.message(rule.message);
                    return true;
                } else {
                    self.isValid(true);
                    self.message("");
                }
            });
        };

        this.validateSilently = function (value) {
            self.rules.some(function (rule) {
                if (!rule.validate(value)) {
                    self.isValid(false);
                    return true;
                } else {
                    self.isValid(true);
                }
            });
        };

        this.setOptions(options);
    }

    return {
        create: function (options) {
            var validator = new Validator(options);
            return validator;
        },
        applyTo: function (itemOrItems, options) {
            var self = this;

            function applyToItem(item) {
                var validator = self.create(options);
                item.validator = validator;
            }

            if (itemOrItems instanceof Array) {
                itemOrItems.forEach(function (item) {

                    applyToItem(item);
                });
            } else {
                applyToItem(itemOrItems);
            }
        },
        applyToProperties: function (item, options) {
            var items = [];

            for (var property in item) {
                if (item.hasOwnProperty(property)) {
                    items.push(item[property]);
                }
            }
            this.applyTo(items, options);
        }
    }
})();
if (typeof ko !== 'undefined') {
    Bifrost.namespace("Bifrost.validation", {
        ValidationSummary: function (commands, containerElement) {
            var self = this;
            this.commands = ko.observable(commands);
            this.messages = ko.observableArray([]);
            this.hasMessages = ko.computed(function(){
                return this.messages().length > 0
            },self);

            function aggregateMessages() {
                var actualMessages = [];
                self.commands().forEach(function (command) {
                    var unwrappedCommand = ko.utils.unwrapObservable(command);

                    unwrappedCommand.validators().forEach(function (validator) {
                        if (!validator.isValid() && validator.message().length) {
                            actualMessages.push(validator.message());
                        }
                    });
                });
                self.messages(actualMessages);
            }

            commands.forEach(function (command) {
                var unwrappedCommand = ko.utils.unwrapObservable(command);

                unwrappedCommand.validators().forEach(function (validator) {
                    validator.message.subscribe(aggregateMessages);
                }); 
            });
        }
    });

    ko.bindingHandlers.validationSummaryFor = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var target = ko.bindingHandlers.validationSummaryFor.getValueAsArray(valueAccessor);
            var validationSummary = new Bifrost.validation.ValidationSummary(target);
            var ul = document.createElement("ul");
            element.appendChild(ul);
            ul.innerHTML = "<li><span data-bind='text: $data'></span></li>";

            ko.utils.domData.set(element, 'validationsummary', validationSummary);
            
            ko.applyBindingsToNode(element, { visible: validationSummary.hasMessages }, validationSummary);
            ko.applyBindingsToNode(ul, { foreach: validationSummary.messages }, validationSummary);
        },
        update: function (element, valueAccessor) {
            var validationSummary = ko.utils.domData.get(element, 'validationsummary');
            validationSummary.commands( ko.bindingHandlers.validationSummaryFor.getValueAsArray(valueAccessor) );
        },
        getValueAsArray: function (valueAccessor) {
            var target = ko.utils.unwrapObservable(valueAccessor());
            if (!(Bifrost.isArray(target))) { target = [target]; }
            return target;
        }
    };
}
if (typeof ko !== 'undefined') {
    ko.bindingHandlers.validationMessageFor = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var value = valueAccessor();
            var validator = value.validator;

            validator.isValid.subscribe(function (newValue) {
                if (newValue == true) {
                    $(element).hide();
                } else {
                    $(element).show();
                }
            });
            ko.applyBindingsToNode(element, { text: validator.message }, validator);
        }
    };
}
if (typeof ko !== 'undefined') {
    ko.extenders.validation = function (target, options) {
        Bifrost.validation.Validator.applyTo(target, options);
        target.subscribe(function (newValue) {
            target.validator.validate(newValue);
        });

        // Todo : look into aggressive validation
        //target.validator.validate(target());
        return target;
    };
}
Bifrost.namespace("Bifrost.validation", {
    required: Bifrost.validation.Rule.extend(function () {
        var self = this;

        this.validate = function (value) {
            return !(Bifrost.isUndefined(value) || Bifrost.isNull(value) || value == "");
        }
    })
});

Bifrost.namespace("Bifrost.validation", {
    minLength: Bifrost.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return Bifrost.isUndefined(value) || Bifrost.isNull(value);
        }

        function throwIfValueIsNotANumber(value) {
            if (!Bifrost.isNumber(value)) {
                throw new Bifrost.validation.NotANumber("Value " + value + " is not a number");
            }
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new Bifrost.validation.OptionsNotDefined();
            }
            if (notSet(options.length)) {
                throw new Bifrost.validation.MaxNotSpecified();
            }
            throwIfValueIsNotANumber(options.length)
        }


        function throwIfValueIsNotAString(string) {
            if (!Bifrost.isString(string)) {
                throw new Bifrost.validation.NotAString("Value " + string + " is not a string");
            }
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIfValueIsNotAString(value);
            return value.length >= self.options.length;
        };
    })
});

Bifrost.namespace("Bifrost.validation", {
    maxLength: Bifrost.validation.Rule.extend(function() {
        var self = this;

        function notSet(value) {
            return Bifrost.isUndefined(value) || Bifrost.isNull(value);
        }

        function throwIfValueIsNotANumber(value) {
            if (!Bifrost.isNumber(value)) {
                throw new Bifrost.validation.NotANumber("Value " + value + " is not a number");
            }
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new Bifrost.validation.OptionsNotDefined();
            }
            if (notSet(options.length)) {
                throw new Bifrost.validation.MaxNotSpecified();
            }
            throwIfValueIsNotANumber(options.length)
        }

    
        function throwIfValueIsNotAString(string) {
            if (!Bifrost.isString(string)) {
                throw new Bifrost.validation.NotAString("Value " + string + " is not a string");
            }
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIfValueIsNotAString(value);
            return value.length <= self.options.length;
        };
    })
});

Bifrost.namespace("Bifrost.validation", {
    range: Bifrost.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return Bifrost.isUndefined(value) || Bifrost.isNull(value);
        }

        function throwIfValueIsNotANumber(value, param) {
            if (!Bifrost.isNumber(value)) {
                throw new Bifrost.validation.NotANumber(param + " value " + value + " is not a number");
            }
        }


        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new Bifrost.validation.OptionsNotDefined();
            }
            if (notSet(options.max)) {
                throw new Bifrost.validation.MaxNotSpecified();
            }
            if (notSet(options.min)) {
                throw new Bifrost.validation.MinNotSpecified();
            }
            throwIfValueIsNotANumber(options.min, "min")
            throwIfValueIsNotANumber(options.max, "max")
        }


        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIfValueIsNotANumber(value, "value");
            return self.options.min <= value && value <= self.options.max;
        };

    })
});
Bifrost.namespace("Bifrost.validation", {
    lessThan: Bifrost.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return Bifrost.isUndefined(value) || Bifrost.isNull(value);
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new Bifrost.validation.OptionsNotDefined();
            }
            if (notSet(options.value)) {
                var exception = new Bifrost.validation.OptionsValueNotSpecified();
                exception.message = exception.message + " 'value' is not set."
                throw exception;
            }
        }

        function throwIsValueToCheckIsNotANumber(value) {
            if (!Bifrost.isNumber(value)) {
                throw new Bifrost.validation.NotANumber("Value " + value + " is not a number");
            }
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIsValueToCheckIsNotANumber(value);
            return parseFloat(value) < parseFloat(self.options.value);
        };
    })
});
Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.lessThanOrEqual = {
    throwIfOptionsInvalid: function (options) {
        if (this.notSet(options)) {
            throw new Bifrost.validation.OptionsNotDefined();
        }
        if (this.notSet(options.value)) {
            var exception = new Bifrost.validation.OptionsValueNotSpecified();
            exception.message = exception.message + " 'value' is not set."
            throw exception;
        }
    },

    throwIsValueToCheckIsNotANumber: function (value) {
        if (!Bifrost.isNumber(value)) {
            throw new Bifrost.validation.NotANumber("Value " + value + " is not a number");
        }
    },

    notSet: function (value) {
        return Bifrost.isUndefined(value) || Bifrost.isNull(value);
    },

    validate: function (value, options) {
        this.throwIfOptionsInvalid(options);
        if (this.notSet(value)) {
            return false;
        }
        this.throwIsValueToCheckIsNotANumber(value);
        return parseFloat(value) <= parseFloat(options.value);
    }
};
Bifrost.namespace("Bifrost.validation", {
    greaterThan: Bifrost.validation.Rule.extend(function() {
        var self = this;

        function notSet(value) {
            return Bifrost.isUndefined(value) || Bifrost.isNull(value);
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new Bifrost.validation.OptionsNotDefined();
            }
            if (notSet(options.value)) {
                var exception = new Bifrost.validation.OptionsValueNotSpecified();
                exception.message = exception.message + " 'value' is not set."
                throw exception;
            }
            throwIfValueToCheckIsNotANumber(options.value);
        }
       
        function throwIfValueToCheckIsNotANumber(value) {
            if (!Bifrost.isNumber(value)) {
                throw new Bifrost.validation.NotANumber("Value " + value + " is not a number");
            }
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIfValueToCheckIsNotANumber(value);
            return parseFloat(value) > parseFloat(self.options.value);
        };
    })
});
Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.greaterThanOrEqual = {
    throwIfOptionsInvalid: function (options) {
        if (this.notSet(options)) {
            throw new Bifrost.validation.OptionsNotDefined();
        }
        if (this.notSet(options.value)) {
            var exception = new Bifrost.validation.OptionsValueNotSpecified();
            exception.message = exception.message + " 'value' is not set."
            throw exception;
        }
        this.throwIfValueToCheckIsNotANumber(options.value);
    },
       
    throwIfValueToCheckIsNotANumber: function (value) {
        if (!Bifrost.isNumber(value)) {
            throw new Bifrost.validation.NotANumber("Value " + value + " is not a number");
        }
    },

    notSet: function (value) {
        return Bifrost.isUndefined(value) || Bifrost.isNull(value);
    },

    validate: function (value, options) {
        this.throwIfOptionsInvalid(options);
        if (this.notSet(value)) {
            return false;
        }
        this.throwIfValueToCheckIsNotANumber(value);
        return parseFloat(value) >= parseFloat(options.value);
    }
};
Bifrost.namespace("Bifrost.validation", {
    email: Bifrost.validation.Rule.extend(function () {
        var self = this;
        var regex = /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$/;

        function notSet(value) {
            return Bifrost.isNull(value) || Bifrost.isUndefined(value);
        }

        this.validate = function (value) {
            if (notSet(value)) {
                return false;
            }

            if (!Bifrost.isString(value)) {
                throw new Bifrost.validation.NotAString("Value " + value + " is not a string");
            }

            return (value.match(regex) == null) ? false : true;
        };

    })
});
Bifrost.namespace("Bifrost.validation", {
    regex: Bifrost.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return Bifrost.isUndefined(value) || Bifrost.isNull(value);
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new Bifrost.validation.OptionsNotDefined();
            }
            if (notSet(options.expression)) {
                throw new Bifrost.validation.MissingExpression();
            }
            if (!Bifrost.isString(options.expression)) {
                throw new Bifrost.validation.NotAString("Expression " + options.expression+ " is not a string.");
            }
        }

        function throwIfValueIsNotString(value) {
            if (!Bifrost.isString(value)) {
                throw new Bifrost.validation.NotAString("Value " + value + " is not a string.");
            }
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIfValueIsNotString(value);
            return (value.match(self.options.expression) == null) ? false : true;
        };
    })
});


if (typeof ko !== 'undefined') {
    ko.bindingHandlers.command = {
        init: function (element, valueAccessor, allBindingAccessor, viewModel) {
			var value = valueAccessor();
			var command;
			var contextBound = false;
			if( typeof value.canExecute === "undefined" ) {
				command = value.target;
				
				command.parameters = command.parameters || {};
				var parameters = value.parameters || {};
				
				for( var parameter in parameters ) {
					var parameterValue = parameters[parameter];
					
					if( command.parameters.hasOwnProperty(parameter) &&
						ko.isObservable(command.parameters[parameter]) ) {
						command.parameters[parameter](parameterValue);
					} else {
						command.parameters[parameter] = ko.observable(parameterValue);
					}
				}
				contextBound = true;
			} else {
				command = value;
			}
            ko.applyBindingsToNode(element, { click: function() {
				// TODO: Investigate further - idea was to support a "context-sensitive" way of dynamically inserting 
				// parameters before execution of the command
				/*
				if( !contextBound ) {
					command.parameters = command.parameters || {};					
					for( var parameter in command.parameters ) {
						if( viewModel.hasOwnProperty(parameter) ) {
							var parameterValue = viewModel[parameter];
							if( ko.isObservable(command.parameters[parameter]) ) {
								command.parameters[parameter](parameterValue);
							} else {
								command.parameters[parameter] = parameterValue;								
							}
						}
					}
				}
				*/
	
				command.execute();
			}}, viewModel);
        }
    };
}
Bifrost.namespace("Bifrost.commands", {
    commandCoordinator: Bifrost.Singleton(function () {
        var baseUrl = "/Bifrost/CommandCoordinator";
        function sendToHandler(url, data, completeHandler) {
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                data: data,
                contentType: 'application/json; charset=utf-8',
                complete: completeHandler
            });
        }

        function handleCommandCompletion(jqXHR, command, commandResult) {
            if (jqXHR.status === 200) {
                command.result = Bifrost.commands.CommandResult.createFrom(commandResult);
                command.hasExecuted = true;
                if (command.result.success === true) {
                    command.onSuccess();
                } else {
                    command.onError();
                }
            } else {
                command.result.success = false;
                command.result.exception = {
                    Message: jqXHR.responseText,
                    details: jqXHR
                };
                command.onError();
            }
            command.onComplete();
        }


        this.handle = function (command) {
            var promise = Bifrost.execution.Promise.create();
            var commandDescriptor = Bifrost.commands.CommandDescriptor.createFrom(command);
            var methodParameters = {
                commandDescriptor: JSON.stringify(commandDescriptor)
            };

            sendToHandler(baseUrl + "/Handle?_cmd=" + command.generatedFrom, JSON.stringify(methodParameters), function (jqXHR) {
                var commandResult = Bifrost.commands.CommandResult.createFrom(jqXHR.responseText);
                promise.signal(commandResult);
            });

            return promise;
        };

        this.handleMany = function (commands) {
            var promise = Bifrost.execution.Promise.create();
            var commandDescriptors = [];

            commands.forEach(function (command) {
                command.isBusy(true);
                var commandDescriptor = Bifrost.commands.CommandDescriptor.createFrom(command);
                commandDescriptors.push(commandDescriptor);
            });

            try {
                var methodParameters = {
                    commandDescriptors: JSON.stringify(commandDescriptors)
                };

                sendToHandler(baseUrl + "/HandleMany", JSON.stringify(methodParameters), function (jqXHR) {
                    var results = JSON.parse(jqXHR.responseText);

                    var commandResults = [];

                    results.forEach(function (result) {
                        var commandResult = Bifrost.commands.CommandResult.createFrom(result);
                        commandResults.push(commandResult);
                    });

                    commands.forEach(function(command, index) {
                        command.handleCommandResult(commandResults[index]);
                        command.isBusy(false);
                    });

                    promise.signal(commandResults);
                });
            } catch(e) {
                commands.forEach(function(command) {
                    command.isBusy(false);
                });
            }

            return promise;
        };

        this.handleForSaga = function (saga, commands, options) {
            throw "not implemented";
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.commandCoordinator = Bifrost.commands.commandCoordinator;
Bifrost.namespace("Bifrost.commands", {
    commandValidationService: Bifrost.Singleton(function () {
        var self = this;

        function shouldSkipProperty(target, property) {
            if (!target.hasOwnProperty(property)) return true;
            if (ko.isObservable(target[property])) return false;
            if (typeof target[property] === "function") return true;
            if (property == "_type") return true;
            if (property == "_namespace") return true;
            if ((target[property].prototype != null) && (target[property] instanceof Bifrost.Type)) return true;

            return false;
        }

        function extendProperties(target, validators) {
            for (var property in target) {
                if (shouldSkipProperty(target, property)) continue;
                if (typeof target[property].validator != "undefined") continue;

                if (ko.isObservable(target[property])) {
                    target[property].extend({ validation: {} });
                    target[property].validator.propertyName = property;
                } else if (typeof target[property] === "object") {
                    extendProperties(target[property], validators);
                }
            }
        }

        function validatePropertiesFor(target, result, silent) {
            for (var property in target) {
                if (shouldSkipProperty(target, property)) continue;

                if (typeof target[property].validator !== "undefined") {
                    if (silent === true) {
                        target[property].validator.validateSilently(target[property]());
                    } else {
                        target[property].validator.validate(target[property]());
                    }

                    if (target[property].validator.isValid() == false) {
                        result.valid = false;
                    }
                } else if (typeof target[property] === "object") {
                    validatePropertiesFor(target[property], result, silent);
                }
            }
        }


        function applyValidationMessageToMembers(command, members, message) {
            for (var memberIndex = 0; memberIndex < members.length; memberIndex++) {
                var path = members[memberIndex].split(".");
                var property = null;
                var target = command;
                path.forEach(function (member) {
                    property = member.toCamelCase();
                    if (property in target) {
                        if (typeof target[property] === "object") {
                            target = target[property];
                        }
                    }
                });

                if (property != null && property.length) {
                    var member = target[property];

                    if (typeof member.validator !== "undefined") {
                        member.validator.isValid(false);
                        member.validator.message(message);
                    }
                }

            }
        }

        this.applyValidationResultToProperties = function (command, validationResults) {

            for (var i = 0; i < validationResults.length; i++) {
                var validationResult = validationResults[i];
                var message = validationResult.errorMessage;
                var memberNames = validationResult.memberNames;
                if (memberNames.length > 0) {
                    applyValidationMessageToMembers(command, memberNames, message);
                }
            }
        };

        this.validate = function (command) {
            var result = { valid: true };
            validatePropertiesFor(command, result);
            return result;
        };
        
        this.validateSilently = function (command) {
            var result = { valid: true };
            validatePropertiesFor(command, result, true);
            return result;
        };

        this.extendPropertiesWithoutValidation = function (command) {
            extendProperties(command);
        };


        function collectValidators(source, validators) {
            for (var property in source) {
                var value = source[property];

                if (shouldSkipProperty(source, property)) continue;

                if (ko.isObservable(value) && typeof value.validator != "undefined") {
                    validators.push(value.validator);
                } else if (Bifrost.isObject(value)) {
                    collectValidators(value, validators);
                }
            }
        }

        this.getValidatorsFor = function (command) {
            var validators = [];
            collectValidators(command, validators);
            return validators;
        };
    })
});
Bifrost.namespace("Bifrost.commands", {
    Command: Bifrost.Type.extend(function (commandCoordinator, commandValidationService, commandSecurityService, options) {
        var self = this;
        this.name = "";
        this.generatedFrom = "";
        this.targetCommand = this;
        this.validators = ko.observableArray();
        this.hasChangesObservables = ko.observableArray();
        this.validationMessages = ko.observableArray();
        this.securityContext = ko.observable(null);
        this.populatedFromExternalSource = ko.observable(false);

        this.isBusy = ko.observable(false);
        this.isValid = ko.computed(function () {
            var valid = true;
            self.validators().some(function (validator) {
                if (ko.isObservable(validator.isValid) && validator.isValid() == false) {
                    valid = false;
                    return true;
                }
            });

            if (self.validationMessages().length > 0) {
                return false;
            }

            return valid;
        });
        this.isAuthorized = ko.observable(false);
        this.canExecute = ko.computed(function () {
            return self.isValid() && self.isAuthorized();
        });
        this.isPopulatedExternally = ko.observable(false);
        this.isReady = ko.computed(function () {
            if (self.isPopulatedExternally() == false) {
                return true;
            }
            return self.populatedFromExternalSource();
        });

        this.hasChanges = ko.computed(function () {
            var hasChange = false;
            self.hasChangesObservables().some(function (item) {
                if (item() === true) {
                    hasChange = true;
                    return true;
                }
            });

            return hasChange;
        });

        this.failedCallbacks = [];
        this.succeededCallbacks = [];
        this.completedCallbacks = [];

        this.commandCoordinator = commandCoordinator;
        this.commandValidationService = commandValidationService;
        this.commandSecurityService = commandSecurityService;

        this.options = {
            beforeExecute: function () { },
            failed: function () { },
            succeeded: function () { },
            completed: function () { },
            properties: {}
        };

        this.failed = function (callback) {
            self.failedCallbacks.push(callback);
            return self;
        };
        this.succeeded = function (callback) {
            self.succeededCallbacks.push(callback);
            return self;
        };
        this.completed = function (callback) {
            self.completedCallbacks.push(callback);
            return self;
        };

        this.setOptions = function (options) {
            Bifrost.extend(self.options, options);
            if (typeof options.name !== "undefined" && typeof options.name === "string") {
                self.name = options.name;
            }
        };

        this.copyPropertiesFromOptions = function () {
            for (var property in self.targetCommand.options.properties) {
                var value = self.targetCommand.options.properties[property];
                if (!ko.isObservable(value)) {
                    value = ko.observable(value);
                }

                self.targetCommand[property] = value;
            }
        };

        this.getProperties = function () {
            var properties = [];
            for (var property in self.targetCommand) {
                if (self.targetCommand.hasOwnProperty(property) && 
                    !(self.hasOwnProperty(property))) {
                    properties.push(property);
                }
            }

            return properties;
        };

        this.makePropertiesObservable = function () {
            var properties = self.getProperties();
            properties.forEach(function (property) {
                var value = null;
                var propertyValue = self.targetCommand[property];

                if (!ko.isObservable(propertyValue) &&
                     (typeof propertyValue != "object" || Bifrost.isArray(propertyValue))) {

                    if (typeof propertyValue !== "function") {
                        if (Bifrost.isArray(propertyValue)) {
                            value = ko.observableArray(propertyValue);
                        } else {
                            value = ko.observable(propertyValue);
                        }
                        self.targetCommand[property] = value;
                    }
                }
            });
        };

        this.extendPropertiesWithHasChanges = function () {
            var properties = self.getProperties();
            properties.forEach(function(property) {
                var propertyValue = self.targetCommand[property];
                if (ko.isObservable(propertyValue)) {
                    propertyValue.extend({ hasChanges: {} })
                    self.hasChangesObservables.push(propertyValue.hasChanges);
                }
            });
        };

        this.onBeforeExecute = function () {
            self.options.beforeExecute();
        };

        this.onFailed = function (commandResult) {
            self.options.failed(commandResult);

            self.failedCallbacks.forEach(function (callback) {
                callback(commandResult);
            });
        };

        this.setInitialValuesFromCurrentValues = function () {
            var properties = self.getProperties();
            properties.forEach(function (propertyName) {
                var property = self.targetCommand[propertyName];
                if (ko.isObservable(property) && ko.isWriteableObservable(property)) {
                    var value = property();
                    property.setInitialValue(value);
                }
            });
        };


        this.onSucceeded = function (commandResult) {
            self.options.succeeded(commandResult);

            self.setInitialValuesFromCurrentValues();

            self.succeededCallbacks.forEach(function (callback) {
                callback(commandResult);
            });
        };

        this.onCompleted = function (commandResult) {
            self.options.completed(commandResult);

            self.completedCallbacks.forEach(function (callback) {
                callback(commandResult);
            });
        };

        this.handleCommandResult = function (commandResult) {
            self.isBusy(false);
            if (typeof commandResult.commandValidationMessages !== "undefined") {
                self.validationMessages(commandResult.commandValidationMessages);
            }

            if (commandResult.success === false || commandResult.invalid === true) {
                if (commandResult.invalid && typeof commandResult.validationResults !== "undefined") {
                    self.commandValidationService.applyValidationResultToProperties(self.targetCommand, commandResult.validationResults);
                }
                self.onFailed(commandResult);
            } else {
                self.onSucceeded(commandResult);
            }
            self.onCompleted(commandResult);
        };

        this.getCommandResultFromValidationResult = function (validationResult) {
            var result = Bifrost.commands.CommandResult.create();
            result.invalid = true;
            return result;
        };

        this.execute = function () {
            self.isBusy(true);
            try {
                self.onBeforeExecute();
                var validationResult = self.commandValidationService.validate(this);
                if (validationResult.valid === true) {
                        self.commandCoordinator.handle(self.targetCommand).continueWith(function (commandResult) {
                            self.handleCommandResult(commandResult);
                        });
                } else {
                    var commandResult = self.getCommandResultFromValidationResult(validationResult);
                    self.handleCommandResult(commandResult);
                }
            } catch (ex) {
                self.isBusy(false);
            }
        };

        this.populatedExternally = function () {
            self.isPopulatedExternally(true);
        };

        this.populateFromExternalSource = function (values) {
            self.isPopulatedExternally(true);
            self.setPropertyValuesFrom(values);
            self.populatedFromExternalSource(true);
        };

        function setValueOnObservable(observable, value) {
            observable(value);

            if (typeof observable.setInitialValue == "function") {
                observable.setInitialValue(value);
            }
        }


        this.setPropertyValuesFrom = function (values) {
            var properties = this.getProperties();

            for (var valueProperty in values) {
                properties.forEach(function (property) {
                    if (valueProperty == property) {
                        var value = ko.utils.unwrapObservable(values[property]);
                        var observable = self.targetCommand[property];

                        if (!ko.isObservable(observable)) {

                            for (var subProperty in observable) {
                                setValueOnObservable(observable[subProperty], value[subProperty]);
                            }
                        } else {
                            setValueOnObservable(observable, value);
                        }
                    }
                });
            }
        };

        this.onCreated = function (lastDescendant) {
            self.targetCommand = lastDescendant;
            if (typeof options !== "undefined") {
                this.setOptions(options);
                this.copyPropertiesFromOptions();
            }
            this.makePropertiesObservable();
            this.extendPropertiesWithHasChanges();
            if (typeof lastDescendant.name !== "undefined" && lastDescendant.name != "") {
                commandValidationService.extendPropertiesWithoutValidation(lastDescendant);
                var validators = commandValidationService.getValidatorsFor(lastDescendant);
                if (Bifrost.isArray(validators) && validators.length > 0) self.validators(validators);
                commandValidationService.validateSilently(this);
            }
            
            if (lastDescendant.securityContext() == null) {
                commandSecurityService.getContextFor(lastDescendant).continueWith(function (securityContext) {
                    lastDescendant.securityContext(securityContext);

                    if (ko.isObservable(securityContext.isAuthorized)) {
                        lastDescendant.isAuthorized(securityContext.isAuthorized());
                        securityContext.isAuthorized.subscribe(function (newValue) {
                            lastDescendant.isAuthorized(newValue);
                        });
                    }
                });
            }
        };
    })
});
Bifrost.namespace("Bifrost.commands");
Bifrost.commands.CommandDescriptor = function(command) {
    var self = this;

    var builtInCommand = {};
    if (typeof Bifrost.commands.Command !== "undefined") {
        builtInCommand = Bifrost.commands.Command.create();
    }

    function shouldSkipProperty(target, property) {
        if (!target.hasOwnProperty(property)) return true;
        if (builtInCommand.hasOwnProperty(property)) return true;
        if (ko.isObservable(target[property])) return false;
        if (typeof target[property] === "function") return true;
        if (property == "_type") return true;
        if (property == "_namespace") return true;

        return false;
    }

    function getPropertiesFromCommand(command) {
        var properties = {};

        for (var property in command) {
            if (!shouldSkipProperty(command, property) ) {
                properties[property] = command[property];
            }
        }
        return properties;
    }

    this.name = command.name;
    this.generatedFrom = command.generatedFrom;
    this.id = Bifrost.Guid.create();

    var properties = getPropertiesFromCommand(command);
    var commandContent = ko.toJS(properties);
    commandContent.Id = Bifrost.Guid.create();
    this.command = ko.toJSON(commandContent);
};


Bifrost.commands.CommandDescriptor.createFrom = function (command) {
    var commandDescriptor = new Bifrost.commands.CommandDescriptor(command);
    return commandDescriptor;
};

Bifrost.namespace("Bifrost.commands");
Bifrost.commands.CommandResult = (function () {
    function CommandResult(existing) {
        var self = this;
        this.isEmpty = function () {
            return self.commandId === Bifrost.Guid.empty;
        };

        if (typeof existing !== "undefined") {
            Bifrost.extend(this, existing);
        } else {
            this.commandName = "";
            this.commandId = Bifrost.Guid.empty;
            this.validationResults = [];
            this.success = true;
            this.invalid = false;
            this.exception = undefined;
        }
    }

    return {
        create: function() {
            var commandResult = new CommandResult();
            return commandResult;
        },
        createFrom: function (result) {
            var existing = typeof result === "string" ? $.parseJSON(result) : result;
            var commandResult = new CommandResult(existing);
            return commandResult;
        }
    };
})();
Bifrost.dependencyResolvers.command = {
    canResolve: function (namespace, name) {
        if (typeof commands !== "undefined") {
            return name in commands;
        }
        return false;
    },

    resolve: function (namespace, name) {
        return commands[name].create();
    }
};
Bifrost.namespace("Bifrost.commands", {
    CommandSecurityContext: Bifrost.Type.extend(function() {
        var self = this;

        this.isAuthorized = ko.observable(false);

    })
});
Bifrost.namespace("Bifrost.commands", {
    commandSecurityContextFactory: Bifrost.Singleton(function () {
        var self = this;

        this.create = function () {
            var context = Bifrost.commands.CommandSecurityContext.create();
            return context;
        };
    })
});
Bifrost.namespace("Bifrost.commands", {
    commandSecurityService: Bifrost.Singleton(function (commandSecurityContextFactory) {
        var self = this;

        this.commandSecurityContextFactory = commandSecurityContextFactory;

        this.getContextFor = function (command) {
            var promise = Bifrost.execution.Promise.create();
            var context = self.commandSecurityContextFactory.create();
            if (typeof command.generatedFrom == "undefined" || command.generatedFrom == "") {
                promise.signal(context);
            } else {
                var url = "/Bifrost/CommandSecurity/GetForCommand?commandName=" + command.generatedFrom;
                $.getJSON(url, function (e) {
                    context.isAuthorized(e.isAuthorized);
                    promise.signal(context);
                });
            }

            return promise;
        };
    })
});
if (typeof ko !== 'undefined') {
    ko.extenders.hasChanges = function (target, options) {
        target._initialValueSet = false;
        target.hasChanges = ko.observable(false);
        function updateHasChanges() {
            if (target._initialValueSet == false) {
                target.hasChanges(false);
            } else {
                if(Bifrost.isArray(target._initialValue)){
                    target.hasChanges(!target._initialValue.shallowEquals(target()));
                    return;
                }
                else
                    target.hasChanges(target._initialValue !== target());
            }
        }

        target.subscribe(function (newValue) {
            updateHasChanges();
        });

        target.setInitialValue = function (value) {
            var initialValue;
            if (Bifrost.isArray(value))
                initialValue = value.clone();
            else
                initialValue = value;
            
            target._initialValue = initialValue;
            target._initialValueSet = true;
            updateHasChanges();
        };
    };
}
Bifrost.namespace("Bifrost.read", {
    queryService: Bifrost.Singleton(function (server, readModelMapper) {
        var self = this;
        this.server = server;
        this.readModelMapper = readModelMapper;

        this.execute = function (query, paging) {
            var promise = Bifrost.execution.Promise.create();

            var url = "/Bifrost/Query/Execute?_q=" + query.generatedFrom;
            self.server.post(url, {
                descriptor: {
                    nameOfQuery: query.name,
                    generatedFrom: query.generatedFrom,
                    parameters: query.getParameterValues()
                },
                paging: {
                    size: paging.size,
                    number: paging.number
                }
            }).continueWith(function (result) {
                if (typeof result == "undefined" || result == null) {
                    result = {};
                }
                if (typeof result.items == "undefined" || result.items == null) result.items = [];
                if (typeof result.totalItems == "undefined" || result.totalItems == null) result.totalItems = 0;

                if (query.hasReadModel()) {
                    result.items = self.readModelMapper.mapDataToReadModel(query.readModel, result.items);
                }
                promise.signal(result);
            });

            return promise;
        };
    })
});
Bifrost.namespace("Bifrost.read", {
	readModelMapper : Bifrost.Type.extend(function () {
		"use strict";
		var self = this;

		function copyProperties (from, to) {
			for (var property in from){
			    if (typeof to[property] !== "undefined") {
			        if (Bifrost.isObject(to[property])) {
			            copyProperties(from[property], to[property]);
					} else {
			            to[property] = from[property];
					}
				}
			}
		}

		function mapSingleInstance(readModel, data) {
		    if (typeof data._readModelType != "undefined") {
		        
		        var readModelType = eval(data._readModelType);
		        if (typeof readModelType != "undefined" && readModelType !== null) {
		            readModel = readModelType;
		        }
		    }

		    var instance = readModel.create();
		    copyProperties(data, instance);
		    return instance;
		};

		function mapMultipleInstances(readModel, data) {
		    var mappedInstances = [];
		    for (var i = 0; i < data.length; i++) {
		        var singleData = data[i];
		        mappedInstances.push(mapSingleInstance(readModel, singleData));
		    }
		    return mappedInstances;
		};

		this.mapDataToReadModel = function(readModel, data) {
			if(Bifrost.isArray(data)){
				return mapMultipleInstances(readModel, data);
			} else {
				return mapSingleInstance(readModel, data);
			}
		};
	})
});
Bifrost.namespace("Bifrost.read", {
    PagingInfo: Bifrost.Type.extend(function (size, number) {
        var self = this;

        this.size = size;
        this.number = number;
    })
});
Bifrost.namespace("Bifrost.read", {
    Queryable: Bifrost.Type.extend(function (query, queryService, targetObservable) {
        var self = this;

        this.canExecute = true;

        this.target = targetObservable;
        this.query = query;
        this.queryService = queryService;
        this.pageSize = ko.observable(0);
        this.pageNumber = ko.observable(0);
        this.totalItems = ko.observable(0);
        this.completedCallbacks = [];

        this.pageSize.subscribe(function () {
            if (self.canExecute) {
                self.execute();
            }
        });

        this.pageNumber.subscribe(function () {
            if (self.canExecute) {
                self.execute();
            }
        });

        function observePropertiesFrom(query) {
            for (var property in query) {
                if (ko.isObservable(query[property]) == true) {
                    query[property].subscribe(function () {
                        self.execute();
                    });
                }
            }
        }

        observePropertiesFrom(query);

        this.completed = function (callback) {
            self.completedCallbacks.push(callback);
            return self;
        };

        this.onCompleted = function (data) {
            self.completedCallbacks.forEach(function (callback) {
                callback(data);
            });
        };

        this.execute = function () {
            if (self.query.areAllParametersSet() !== true) {
                // TODO: Diagnostics - warning
                return self.target;
            }

            var paging = Bifrost.read.PagingInfo.create({
                size: self.pageSize(),
                number: self.pageNumber()
            });
            self.queryService.execute(query, paging).continueWith(function (result) {
                self.totalItems(result.totalItems);
                self.target(result.items);
                self.onCompleted(result.items);
            });

            return self.target;
        };

        this.setPageInfo = function (pageSize, pageNumber) {
            self.canExecute = false;
            self.pageSize(pageSize);
            self.pageNumber(pageNumber);
            self.canExecute = true;
            self.execute();
        };
    })
});
Bifrost.read.Queryable.new = function (options, executeQuery) {
    var observable = ko.observableArray();
    options.targetObservable = observable;
    var queryable = Bifrost.read.Queryable.create(options);
    Bifrost.extend(observable, queryable);
    observable.isQueryable = true;
    return observable;
};


Bifrost.namespace("Bifrost.read", {
    Query: Bifrost.Type.extend(function () {
        var self = this;
        this.name = "";
        this.target = this;
        this.generatedFrom = "";
        this.readModel = null;

        this.areAllParametersSet = null;

        this.hasReadModel = function () {
            return typeof self.target.readModel != "undefined" && self.target.readModel != null;
        };

        this.setParameters = function (parameters) {
            try {
                for (var property in parameters) {
                    if (self.target.hasOwnProperty(property) && ko.isObservable(self.target[property]) == true) {
                        self.target[property](parameters[property]);
                    }
                }
            } catch(ex) {}
        };

        this.getParameters = function () {
            var parameters = {};

            for (var property in self.target) {
                if (ko.isObservable(self.target[property]) &&
                    property != "areAllParametersSet") {
                    parameters[property] = self.target[property];
                }
            }

            return parameters;
        };

        this.getParameterValues = function () {
            var parameterValues = {};

            var parameters = self.getParameters();
            for (var property in parameters) {
                parameterValues[property] = ko.utils.unwrapObservable(parameters[property]);
            }

            return parameterValues;
        };

        this.all = function () {
            var queryable = Bifrost.read.Queryable.new({
                query: self.target
            });
            return queryable;
        };

        this.paged = function (pageSize, pageNumber) {
            var queryable = Bifrost.read.Queryable.new({
                query: self.target
            });
            queryable.setPageInfo(pageSize, pageNumber);
            return queryable;
        };

        

        this.onCreated = function (query) {
            self.target = query;

            self.areAllParametersSet = ko.computed(function () {
                var isSet = true;
                var hasParameters = false;
                for (var property in self.target) {
                    if (ko.isObservable(self.target[property]) == true) {
                        hasParameters = true;
                        var value = self.target[property]();
                        if (typeof value == "undefined" || value === null) {
                            isSet = false;
                            break;
                        }
                    }
                }
                if (hasParameters == false) return true;
                return isSet;
            });
        };
    })
});
Bifrost.namespace("Bifrost.read", {
    ReadModel: Bifrost.Type.extend(function () {
        var self = this;
        var actualReadModel = this;


        this.copyTo = function (target) {
            for (var property in actualReadModel) {
                if (actualReadModel.hasOwnProperty(property) && property.indexOf("_") != 0) {
                    var value = ko.utils.unwrapObservable(actualReadModel[property]);
                    if (!target.hasOwnProperty(property)) {
                        target[property] = ko.observable(value);
                    } else {
                        if (ko.isObservable(target[property])) {
                            target[property](value);
                        } else {
                            target[property] = value;
                        }
                    }
                }
            }
        };

        this.onCreated = function (lastDescendant) {
            actualReadModel = lastDescendant;
        };
    })
});
Bifrost.namespace("Bifrost.read", {
	ReadModelOf: Bifrost.Type.extend(function(readModelMapper) {
	    var self = this;
	    this.name = "";
	    this.generatedFrom = "";
	    this.target = null;
	    this.readModelType = Bifrost.Type.extend(function () { });
	    this.instance = ko.observable();
	    this.commandToPopulate = null;

		this.instanceMatching = function (propertyFilters) {
		    var methodParameters = {
		        descriptor: JSON.stringify({
		            readModel: self.target.name,
                    generatedFrom: self.target.generatedFrom,
		            propertyFilters: propertyFilters
		        })
		    };

		    $.ajax({
		        url: "/Bifrost/ReadModel/InstanceMatching?_rm=" + self.target.generatedFrom,
		        type: 'POST',
		        dataType: 'json',
		        data: JSON.stringify(methodParameters),
		        contentType: 'application/json; charset=utf-8',
		        complete: function (result) {
		            var item = $.parseJSON(result.responseText);
					var mappedReadModel = readModelMapper.mapDataToReadModel(self.target.readModelType, item);
		            self.instance(mappedReadModel);
		        }
		    });
		};

		this.populateCommandOnChanges = function (command) {
		    command.populatedExternally();

		    if (typeof self.instance() != "undefined" && self.instance() != null) {
		        command.populateFromExternalSource(self.instance());
		    }

		    self.instance.subscribe(function (newValue) {
		        command.populateFromExternalSource(newValue);
		    });
		};

		this.onCreated = function (lastDescendant) {
		    self.target = lastDescendant;
		};
	})
});
Bifrost.dependencyResolvers.readModelOf = {
    canResolve: function (namespace, name) {
        if (typeof read !== "undefined") {
            return name in read;
        }
        return false;
    },

    resolve: function (namespace, name) {
        return read[name].create();
    }
};
Bifrost.dependencyResolvers.query = {
    canResolve: function (namespace, name) {
        if (typeof read !== "undefined") {
            return name in read;
        }
        return false;
    },

    resolve: function (namespace, name) {
        return read[name].create();
    }
};
Bifrost.namespace("Bifrost.read", {
    queryService: Bifrost.Singleton(function (server, readModelMapper) {
        var self = this;
        this.server = server;
        this.readModelMapper = readModelMapper;

        this.execute = function (query, paging) {
            var promise = Bifrost.execution.Promise.create();

            var url = "/Bifrost/Query/Execute?_q=" + query.generatedFrom;
            self.server.post(url, {
                descriptor: {
                    nameOfQuery: query.name,
                    generatedFrom: query.generatedFrom,
                    parameters: query.getParameterValues()
                },
                paging: {
                    size: paging.size,
                    number: paging.number
                }
            }).continueWith(function (result) {
                if (typeof result == "undefined" || result == null) {
                    result = {};
                }
                if (typeof result.items == "undefined" || result.items == null) result.items = [];
                if (typeof result.totalItems == "undefined" || result.totalItems == null) result.totalItems = 0;

                if (query.hasReadModel()) {
                    result.items = self.readModelMapper.mapDataToReadModel(query.readModel, result.items);
                }
                promise.signal(result);
            });

            return promise;
        };
    })
});
Bifrost.namespace("Bifrost.sagas");
Bifrost.sagas.Saga = (function () {
    function Saga() {
        var self = this;

        this.executeCommands = function (commands, options) {

            var canExecuteSaga = true;
            
            commands.forEach(function (command) {
                if (command.onBeforeExecute() === false) {
                    canExecuteSaga = false;
                    return false;
                }
            });

            if (canExecuteSaga === false) {
                return;
            }
            Bifrost.commands.commandCoordinator.handleForSaga(self, commands, options);
        }
    }

    return {
        create: function (configuration) {
            var saga = new Saga();
            Bifrost.extend(saga, configuration);
            return saga;
        }
    }
})();
Bifrost.namespace("Bifrost.sagas");
Bifrost.sagas.sagaNarrator = (function () {
    var baseUrl = "/Bifrost/SagaNarrator";
    // Todo : abstract away into general Service code - look at CommandCoordinator.js for the other copy of this!s
    function post(url, data, completeHandler) {
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'json',
            data: data,
            contentType: 'application/json; charset=utf-8',
            complete: completeHandler
        });
    }

    function isRequestSuccess(jqXHR, commandResult) {
        if (jqXHR.status === 200) {
            if (commandResult.success === true) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
        return true;
    }

    return {
        conclude: function (saga, success, error) {
            var methodParameters = {
                sagaId: saga.Id
            };
            post(baseUrl + "/Conclude", JSON.stringify(methodParameters), function (jqXHR) {
                var commandResult = Bifrost.commands.CommandResult.createFrom(jqXHR.responseText);
                var isSuccess = isRequestSuccess(jqXHR, commandResult);
                if (isSuccess == true && typeof success === "function") {
                    success(saga);
                }
                if (isSuccess == false && typeof error === "function") {
                    error(saga);
                }
            });
        }
    }
})();
Bifrost.namespace("Bifrost.messaging", {
    Messenger: Bifrost.Type.extend(function () {
        var subscribers = [];

        this.publish = function (topic, message) {
            if (subscribers.hasOwnProperty(topic)) {
                subscribers[topic].subscribers.forEach(function (item) {
                    item(message);
                });
            }
        };

        this.subscribeTo = function (topic, subscriber) {
            var subscribersByTopic;

            if (subscribers.hasOwnProperty(topic)) {
                subscribersByTopic = subscribers[topic];
            } else {
                subscribersByTopic = { subscribers: [] };
                subscribers[topic] = subscribersByTopic;
            }

            subscribersByTopic.subscribers.push(subscriber);
        };

        return {
            publish: this.publish,
            subscribeTo: this.subscribeTo
        };
    })
});
Bifrost.messaging.Messenger.global = Bifrost.messaging.Messenger.create();
Bifrost.WellKnownTypesDependencyResolver.types.globalMessenger = Bifrost.messaging.Messenger.global;
if (typeof ko !== 'undefined') {
    ko.observableMessage = function (message, defaultValue) {
        var observable = ko.observable(defaultValue);

        var internal = false;
        observable.subscribe(function (newValue) {
            if (internal == true) return;
            Bifrost.messaging.Messenger.global.publish(message, newValue);
        });

        Bifrost.messaging.Messenger.global.subscribeTo(message, function (value) {
            internal = true;
            observable(value);
            internal = false;
        });
        return observable;
    }
}
Bifrost.namespace("Bifrost.services", {
    Service: Bifrost.Type.extend(function () {
        var self = this;

        this.url = "";
        this.name = "";

        function prepareArguments(args) {
            var prepared = {};

            for (var property in args) {
                prepared[property] = JSON.stringify(args[property]);
            }

            var stringified = JSON.stringify(prepared);
            return stringified;
        }

        function call(method, args, callback) {
            $.ajax({
                url: self.url + "/" + method,
                type: 'POST',
                dataType: 'json',
                data: prepareArguments(args),
                contentType: 'application/json; charset=utf-8',
                complete: function (result) {
                    var v = $.parseJSON(result.responseText);
                    callback(v);
                }
            });
        }


        this.callWithoutReturnValue = function (method, args) {
            var promise = Bifrost.execution.Promise.create();
            call(method, args, function (v) {
                promise.signal();
            });
            return promise;
        };

        this.callWithObjectAsReturn = function (method, args) {
            var value = ko.observable();
            call(method, args, function (v) {
                value(v);
            });
            return value;
        };

        this.callWithArrayAsReturn = function (method, args) {
            var value = ko.observableArray();
            call(method, args, function (v) {
                value(v);
            });
            return value;
        };

        this.onCreated = function (lastDescendant) {
            self.url = lastDescendant.url;
            if (self.url.indexOf("/") != 0) self.url = "/" + self.url;

            self.name = lastDescendant.name;
        };
    })
});
Bifrost.dependencyResolvers.service = {
    canResolve: function (namespace, name) {
        if (typeof services !== "undefined") {
            return name in services;
        }
        return false;
    },

    resolve: function (namespace, name) {
        return services[name].create();
    }
};
Bifrost.namespace("Bifrost.views", {
    documentService: Bifrost.Singleton(function (DOMRoot) {
        var self = this;

        this.DOMRoot = DOMRoot;

        this.getAllElementsWithViewModelFiles = function () {
            return self.getAllElementsWithViewModelFilesFrom(self.DOMRoot);
        };

        this.getAllElementsWithViewModelFilesFrom = function (root) {
            var elements = [];
            if (typeof $(root).data("viewmodel-file") != "undefined") {
                elements.push(root);
            }
            $("[data-viewmodel-file]",root).each(function () {
                elements.push(this);
            });
            return elements;
        };

        function collectViewModelFilesFrom(parent, elements) {

            if (typeof parent.childNodes != "undefined") {
                parent.childNodes.forEach(function (child) {
                    collectViewModelFilesFrom(child, elements);
                });
            }

            var viewModelFile = $(parent).data("viewmodel-file");
            if (typeof viewModelFile != "undefined") {
                elements.push(parent);
            }
        }

        this.getAllElementsWithViewModelFilesSorted = function () {
            return self.getAllElementsWithViewModelFilesSortedFrom(self.DOMRoot);
        };

        this.getAllElementsWithViewModelFilesSortedFrom = function (root) {
            var elements = [];
            collectViewModelFilesFrom(root, elements);
            return elements;
        };

        this.getViewModelFileFrom = function (element) {
            var file = $(element).data("viewmodel-file");
            if (typeof file == "undefined") file = "";
            return file;
        };

        this.setViewModelFileOn = function (element, file) {
            $(element).data("viewmodel-file", file);
            $(element).attr("data-viewmodel-file", file);
        };

        this.setViewModelOn = function (element, viewModel) {
            element.viewModel = viewModel;
            $(element).data("viewmodel", viewModel);
        };

        this.getViewModelFrom = function (element) {
            return element.viewModel;
        };
    })
});
Bifrost.namespace("Bifrost.views", {
    View: Bifrost.Type.extend(function (viewLoader, path) {
        var self = this;
        this.viewLoader = viewLoader;

        this.path = path;
        this.content = "[CONTENT NOT LOADED]";
        this.element = null;


        this.load = function () {
            var promise = Bifrost.execution.Promise.create();
            self.viewLoader.load(self.path).continueWith(function (html) {
                self.content = html;
                promise.signal(self);
            });

            return promise;
        };
    })
});
Bifrost.namespace("Bifrost.views", {
    ViewRenderer: Bifrost.Type.extend(function () {
        this.canRender = function (element) {
            return false;
        };

        this.render = function (element) {
        };
	})
});
Bifrost.namespace("Bifrost.views", {
	viewRenderers: Bifrost.Singleton(function() {
		var self = this;

		function getRenderers() {
			var renderers = [];
			for( var property in Bifrost.views.viewRenderers ) {
				if( Bifrost.views.viewRenderers.hasOwnProperty(property)) {
					var value = Bifrost.views.viewRenderers[property];
					if( typeof value == "function" && 
						typeof value.create == "function")  {
						var renderer = value.create();
						if( typeof renderer.canRender == "function") renderers.push(renderer);
					}
				}
			}
			return renderers;
		}

		this.canRender = function(element) {
		    var renderers = getRenderers();
		    for (var rendererIndex = 0; rendererIndex < renderers.length; rendererIndex++) {
		        var renderer = renderers[rendererIndex];
		        var result = renderer.canRender(element);
				if( result == true ) return true;
			}

			return false;
		};

		this.render = function(element) {
		    var renderers = getRenderers();
		    for (var rendererIndex = 0; rendererIndex < renderers.length; rendererIndex++) {
		        var renderer = renderers[rendererIndex];
		        if (renderer.canRender(element)) return renderer.render(element);
			}

			return null;
		};

	})
});
Bifrost.namespace("Bifrost.views", {
	DataAttributeViewRenderer : Bifrost.views.ViewRenderer.extend(function(viewFactory, pathResolvers, viewModelManager) {
	    var self = this;

	    this.viewFactory = viewFactory;
	    this.pathResolvers = pathResolvers;
	    this.viewModelManager = viewModelManager;

		this.canRender = function(element) {
		    return typeof $(element).data("view") !== "undefined";
		};

		this.render = function (element) {
		    var promise = Bifrost.execution.Promise.create();
		    var path = $(element).data("view");

		    if (self.pathResolvers.canResolve(element, path)) {
		        var actualPath = self.pathResolvers.resolve(element, path);
		        var view = self.viewFactory.createFrom(actualPath);

		        view.element = element;
		        view.load().continueWith(function (targetView) {
		            $(element).empty();
		            $(element).append(targetView.content);

		            if (self.viewModelManager.hasForView(actualPath)) {
		                var viewModelFile = Bifrost.path.changeExtension(actualPath, "js");
		                $(element).attr("data-viewmodel-file", viewModelFile);
		                $(element).data("viewmodel-file", viewModelFile);
		            }

		            promise.signal(targetView);
		        });
		    } else {
                // Todo: throw an exception at this point! - Or something like 404.. 
		        promise.signal(null);
		    }

		    return promise;
		};
	})
});
if (typeof Bifrost.views.viewRenderers != "undefined") {
	Bifrost.views.viewRenderers.DataAttributeViewRenderer = Bifrost.views.DataAttributeViewRenderer;
}

Bifrost.namespace("Bifrost.views", {
    viewFactory: Bifrost.Singleton(function () {
        var self = this;

        this.createFrom = function (path) {
            var view = Bifrost.views.View.create({
                path: path
            });
            return view;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.viewFactory = Bifrost.views.viewFactory;
Bifrost.namespace("Bifrost.views", {
    viewLoader: Bifrost.Singleton(function (viewModelManager) {
        var self = this;

        this.viewModelManager = viewModelManager;
        
        this.load = function (path) {
            var promise = Bifrost.execution.Promise.create();

            if (!path.startsWith("/")) path = "/" + path;

            var files = [];

            var viewFile = "text!" + path + "!strip";
            if (!Bifrost.path.hasExtension(viewFile)) viewFile = "noext!" + viewFile;
            files.push(viewFile);

            if (self.viewModelManager.hasForView(path)) {
                var viewModelFile = self.viewModelManager.getViewModelPathForView(path);
                files.push(viewModelFile);
            }

            require(files, function (view) {
                promise.signal(view);
            });
            return promise;
        };
    })
});
Bifrost.namespace("Bifrost.views", {
    viewManager: Bifrost.Singleton(function (viewRenderers, viewFactory, pathResolvers, viewModelManager) {
        var self = this;
        
        this.viewRenderers = viewRenderers;
        this.viewFactory = viewFactory;
        this.pathResolvers = pathResolvers;
        this.viewModelManager = viewModelManager;

        function renderChildren(element) {
            if(element.hasChildNodes() == true) {
                for (var child = element.firstChild; child; child = child.nextSibling) {
                    self.render(child);
                }
            }
        }

        this.initializeLandingPage = function () {
            var body = $("body")[0];
            if (body !== null) {
                var file = Bifrost.path.getFilenameWithoutExtension(document.location.toString());
                if (file == "") file = "index";
                $(body).data("view", file);

                if (self.pathResolvers.canResolve(body, file)) {
                    var actualPath = self.pathResolvers.resolve(body, file);
                    var view = self.viewFactory.createFrom(actualPath);
                    view.element = body;
                    view.content = body.innerHTML;

                    // Todo: this one destroys the bubbling of click event to the body tag..  Weird.. Need to investigate more (see GitHub issue 233 : https://github.com/dolittle/Bifrost/issues/233)
                    //self.viewModelManager.applyToViewIfAny(view);

                    renderChildren(body);
                }
            }
        };

        this.render = function (element) {
            var promise = Bifrost.execution.Promise.create();

            if (self.viewRenderers.canRender(element)) {
                self.viewRenderers.render(element).continueWith(function (view) {
                    var newElement = view.element;
                    newElement.view = view;
                    self.viewModelManager.applyToViewIfAny(view);
                    renderChildren(newElement);
                });
            } else {
                renderChildren(element);
            }

            return promise;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.viewManager = Bifrost.views.viewManager;
Bifrost.namespace("Bifrost.views", {
    ViewModel: Bifrost.Type.extend(function () {
        var self = this;
        this.targetViewModel = this;

        this.activated = function () {
            if (typeof self.targetViewModel.onActivated === "function") {
                self.targetViewModel.onActivated();
            }
        };

        this.onCreated = function (lastDescendant) {
            self.targetViewModel = lastDescendant;
        };
    })
});
Bifrost.namespace("Bifrost.views", {
    viewModelLoader: Bifrost.Singleton(function () {
        var self = this;

        this.load = function (path) {
            var promise = Bifrost.execution.Promise.create();
            if (!path.startsWith("/")) path = "/" + path;
            require([path], function () {

                self.beginCreateInstanceOfViewModel(path).continueWith(function (instance) {
                    promise.signal(instance);
                });
            });
            return promise;
        };

        this.beginCreateInstanceOfViewModel = function (path) {
            var localPath = Bifrost.path.getPathWithoutFilename(path);
            var filename = Bifrost.path.getFilenameWithoutExtension(path);

            var promise = Bifrost.execution.Promise.create();

            for (var mapperKey in Bifrost.namespaceMappers) {
                var mapper = Bifrost.namespaceMappers[mapperKey];
                if (typeof mapper.hasMappingFor === "function" && mapper.hasMappingFor(path)) {
                    var namespacePath = mapper.resolve(localPath);
                    var namespace = Bifrost.namespace(namespacePath);

                    if (filename in namespace) {
                        namespace[filename].beginCreate().continueWith(function (instance) {
                            promise.signal(instance);
                        }).onFail(function () {
                            promise.signal({});
                        });
                    }
                }
            }

            return promise;
        };
    })
});
Bifrost.namespace("Bifrost.views", {
    viewModelManager: Bifrost.Singleton(function(assetsManager, documentService, viewModelLoader) {
        var self = this;
        this.assetsManager = assetsManager;
        this.viewModelLoader = viewModelLoader;
        this.documentService = documentService;

        this.masterViewModel = {};


        function applyViewModel(instance, target) {
            var viewModelFile = self.documentService.getViewModelFileFrom(target);
            self.documentService.setViewModelOn(target, instance);
            
            ko.applyBindingsToNode(target, {
                'viewModel': instance
            });

            if (typeof instance.activated == "function") {
                instance.activated();
            }
        }

        function setViewModelBindingExpression(instance, target) {
            var viewModelFile = self.documentService.getViewModelFileFrom(target);
            self.documentService.setViewModelOn(target, instance);

            if (viewModelFile.indexOf(".") > 0) {
                viewModelFile = viewModelFile.substr(0, viewModelFile.indexOf("."));
            }

            var propertyName = viewModelFile.replaceAll("/", "");
            self.masterViewModel[propertyName] = ko.observable(instance);

            $(target).attr("data-bind", "viewModel: $data." + propertyName);
            
            if (typeof instance.activated == "function") {
                instance.activated();
            }
        }

        function applyViewModelsByAttribute(path, container) {
            var viewModelApplied = false;

            var elements = self.documentService.getAllElementsWithViewModelFilesFrom(container);
            if (elements.length > 0) {

                function loadAndApply(target) {
                    viewModelApplied = true;
                    var viewModelFile = $(target).data("viewmodel-file");
                    self.viewModelLoader.load(viewModelFile, path).continueWith(function (instance) {
                        applyViewModel(instance, target, viewModelFile);
                    });
                }

                if (elements.length == 1) {
                    loadAndApply(elements[0]);
                } else {
                    for (var elementIndex = elements.length - 1; elementIndex > 0; elementIndex--) {
                        loadAndApply(elements[elementIndex]);
                    }
                }
            }

            return viewModelApplied;
        }

        function applyViewModelByConventionFromPath(path, container) {
            if (self.hasForView(path)) {
                var viewModelFile = Bifrost.path.changeExtension(path, "js");
                self.documentService.setViewModelFileOn(container, viewModelFile);

                self.viewModelLoader.load(viewModelFile, path).continueWith(function (instance) {
                    applyViewModel(instance, target, viewModelFile);
                });
            }
        }

        this.hasForView = function (viewPath) {
            var scriptFile = Bifrost.path.changeExtension(viewPath, "js");
            scriptFile = Bifrost.path.makeRelative(scriptFile);
            var hasViewModel = self.assetsManager.hasScript(scriptFile);
            return hasViewModel;
        };

        this.getViewModelPathForView = function (viewPath) {
            var scriptFile = Bifrost.path.changeExtension(viewPath, "js");
            return scriptFile;
        };

        this.applyToViewIfAny = function (view) {
            var viewModelApplied = false;

            if (self.hasForView(view.path)) {
                var viewModelFile = Bifrost.path.changeExtension(view.path, "js");
                self.documentService.setViewModelFileOn(view.element,viewModelFile);

                self.viewModelLoader.load(viewModelFile).continueWith(function (instance) {
                    applyViewModel(instance, view.element);
                });
            } else {
                viewModelApplied = applyViewModelsByAttribute(view.path, view.element);
                if (viewModelApplied == false) {
                    applyViewModelByConventionFromPath(view.path, view.element);
                }
            }
        };

        this.loadAndApplyAllViewModelsWithinElement = function (root) {
            var elements = self.documentService.getAllElementsWithViewModelFilesFrom(root);
            var loadedViewModels = 0;

            self.masterViewModel = {};

            elements.forEach(function (element) {
                var viewModelFile = self.documentService.getViewModelFileFrom(element);

                self.viewModelLoader.load(viewModelFile).continueWith(function (instance) {
                    documentService.setViewModelOn(element, instance);

                    loadedViewModels++;

                    if (loadedViewModels == elements.length) {
                        elements.forEach(function (elementToApplyBindingsTo) {
                            var viewModel = self.documentService.getViewModelFrom(elementToApplyBindingsTo);
                            setViewModelBindingExpression(viewModel, elementToApplyBindingsTo);
                        });

                        ko.applyBindings(self.masterViewModel);
                    }
                });
            });
        };

        this.loadAndApplyAllViewModelsInDocument = function () {
            self.loadAndApplyAllViewModelsWithinElement(self.documentService.DOMRoot);
        };
    })
});
Bifrost.namespace("Bifrost.views", {
    PathResolver: Bifrost.Type.extend(function () {
        this.canResolve = function (element, path) {
            return false;
        };

        this.resolve = function (element, path) {
            
        };
    })
});
Bifrost.namespace("Bifrost.views", {
    pathResolvers: Bifrost.Singleton(function () {

        function getResolvers() {
            var resolvers = [];
            for (var property in Bifrost.views.pathResolvers) {
                if (Bifrost.views.pathResolvers.hasOwnProperty(property)) {
                    var value = Bifrost.views.pathResolvers[property];
                    if( typeof value == "function" &&
                        typeof value.create == "function") {

                        var resolver = value.create();
                        if (typeof resolver.canResolve == "function") resolvers.push(resolver);
                    }
                }
            }
            return resolvers;
        }


        this.canResolve = function (element, path) {
            var resolvers = getResolvers();
            for (var resolverIndex = 0; resolverIndex < resolvers.length; resolverIndex++) {
                var resolver = resolvers[resolverIndex];
                var result = resolver.canResolve(element, path);
                if (result == true) return true;
            }
            return false;
        };

        this.resolve = function (element, path) {
            var resolvers = getResolvers();
            for (var resolverIndex = 0; resolverIndex < resolvers.length; resolverIndex++) {
                var resolver = resolvers[resolverIndex];
                if (resolver.canResolve(element, path)) return resolver.resolve(element, path);
            }
            return null;
        };
    })
});
Bifrost.namespace("Bifrost.views", {
    UriMapperPathResolver: Bifrost.views.PathResolver.extend(function () {
        this.canResolve = function (element, path) {
            var closest = $(element).closest("[data-urimapper]");
            if (closest.length == 1) {
                var mapperName = $(closest[0]).data("urimapper");
                if (Bifrost.uriMappers[mapperName].hasMappingFor(path) == true) return true;
            }
            return Bifrost.uriMappers.default.hasMappingFor(path);
        };

        this.resolve = function (element, path) {
            var closest = $(element).closest("[data-urimapper]");
            if (closest.length == 1) {
                var mapperName = $(closest[0]).data("urimapper");
                if (Bifrost.uriMappers[mapperName].hasMappingFor(path) == true) {
                    return Bifrost.uriMappers[mapperName].resolve(path);
                }
            }
            return Bifrost.uriMappers.default.resolve(path);
        };
    })
});
if (typeof Bifrost.views.pathResolvers != "undefined") {
    Bifrost.views.pathResolvers.UriMapperPathResolver = Bifrost.views.UriMapperPathResolver;
}
Bifrost.namespace("Bifrost.views", {
    RelativePathResolver: Bifrost.views.PathResolver.extend(function () {
        this.canResolve = function (element, path) {
            var closest = $(element).closest("[data-view]");
            if (closest.length == 1) {
                var view = $(closest[0]).view;
                
            }
            return false;
        };

        this.resolve = function (element, path) {
            var closest = $(element).closest("[data-urimapper]");
            if (closest.length == 1) {
                var mapperName = $(closest[0]).data("urimapper");
                if (Bifrost.uriMappers[mapperName].hasMappingFor(path) == true) {
                    return Bifrost.uriMappers[mapperName].resolve(path);
                }
            }
            return Bifrost.uriMappers.default.resolve(path);
        };
    })
});
if (typeof Bifrost.views.pathResolvers != "undefined") {
    Bifrost.views.pathResolvers.RelativePathResolver = Bifrost.views.RelativePathResolver;
}
Bifrost.namespace("Bifrost.views", {
    viewModelBindingHandler: Bifrost.Type.extend(function(viewManager, documentService) {
        var self = this;
        this.viewManager = viewManager;
        this.documentService = documentService;

        this.init = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
            var viewModel = self.documentService.getViewModelFrom(element);
            var childBindingContext = bindingContext.createChildContext(viewModel);
            childBindingContext.$root = viewModel;
            ko.applyBindingsToDescendants(childBindingContext, element);
            return { controlsDescendantBindings: true };
        };
        this.update = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
        };
    })
});
Bifrost.views.viewModelBindingHandler.initialize = function () {
    ko.bindingHandlers.viewModel = Bifrost.views.viewModelBindingHandler.create();
};

Bifrost.namespace("Bifrost.views", {
    viewBindingHandler: Bifrost.Type.extend(function (viewRenderers, pathResolvers, viewFactory, viewModelManager) {
        var self = this;
        this.viewRenderers = viewRenderers;
        this.pathResolvers = pathResolvers;
        this.viewFactory = viewFactory;
        this.viewModelManager = viewModelManager;

        function renderChildren(element) {
            if (element.hasChildNodes() == true) {
                for (var child = element.firstChild; child; child = child.nextSibling) {
                    self.render(child);
                }
            }
        }

        this.init = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
        };
        this.update = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
            var uri = ko.utils.unwrapObservable(valueAccessor());

            $(element).data("view", uri);
            if (self.pathResolvers.canResolve(element, uri)) {
                var actualPath = self.pathResolvers.resolve(element, uri);
                var view = self.viewFactory.createFrom(actualPath);
                view.element = element;
                view.content = element.innerHTML;
                self.render(element);

                renderChildren(element);
            }
        };

        this.render = function (element) {
            var promise = Bifrost.execution.Promise.create();

            if (self.viewRenderers.canRender(element)) {
                self.viewRenderers.render(element).continueWith(function (view) {
                    var newElement = view.element;
                    newElement.view = view;
                    self.viewModelManager.applyToViewIfAny(view);
                    renderChildren(newElement);
                });
            } else {
                renderChildren(element);
            }

            return promise;
        };
    })
});
Bifrost.views.viewBindingHandler.initialize = function () {
    ko.bindingHandlers.view = Bifrost.views.viewBindingHandler.create();
};

Bifrost.namespace("Bifrost.navigation", {
    NavigationFrame: Bifrost.Type.extend(function (home, locationAware, uriMapper, history, viewManager) {
        var self = this;

        this.home = home;
        this.locationAware = locationAware || false;
        this.history = history;
        this.viewManager = viewManager;

        this.container = null;
        this.currentUri = ko.observable(home);
        this.currentRenderedPath = null;
        this.uriMapper = uriMapper || null;

        this.currentUri.subscribe(function () {
            self.render();
        });
        
        this.setCurrentUri = function (path) {
            if (path.indexOf("/") == 0) path = path.substr(1);
            if (path == null || path.length == 0) path = self.home;
            if (self.uriMapper != null && !self.uriMapper.hasMappingFor(path)) path = self.home;
            self.currentUri(path);
        };

        this.setCurrentUriFromCurrentLocation = function () {
            var state = self.history.getState();
            var uri = Bifrost.Uri.create(state.url);
            self.setCurrentUri(uri.path);
        }

        if (locationAware === true) {
            history.Adapter.bind(window, "statechange", function () {
                self.setCurrentUriFromCurrentLocation();
            });
        }

        this.setContainer = function (container) {
            if (self.locationAware === true) {
                self.setCurrentUriFromCurrentLocation();
            }

            self.container = container;

            var uriMapper = $(container).closest("[data-urimapper]");
            if (uriMapper.length == 1) {
                var uriMapperName = $(uriMapper[0]).data("urimapper");
                if (uriMapperName in Bifrost.uriMappers) {
                    self.uriMapper = Bifrost.uriMappers[uriMapperName];
                }
            }
            if (self.uriMapper == null) self.uriMapper = Bifrost.uriMappers.default;
            return self.render();
        };

        this.render = function () {
            var promise = Bifrost.execution.Promise.create();
            var path = self.currentUri();
            if (self.container == null) return;
            if (path == self.currentRenderedPath) return;
            self.currentRenderedPath = path;

            if (path !== null && typeof path !== "undefined") {
                $(self.container).data("view", path);
                self.viewManager.render(self.container).continueWith(function (view) {
                    promise.signal(view);
                });
            }
            return promise;
        };

        this.navigate = function (uri) {
            self.setCurrentUri(uri);
        };

    })
});
Bifrost.namespace("Bifrost.navigation", {
    navigationFrames: Bifrost.Singleton(function () {
        var self = this;

        this.hookup = function () {
            $("[data-navigation-frame]").each(function (index, element) {
                var configurationString = $(element).data("navigation-frame");
                var configurationItems = ko.expressionRewriting.parseObjectLiteral(configurationString);

                var configuration = {};

                for (var index = 0; index < configurationItems.length; index++) {
                    var item = configurationItems[index];
                    configuration[item.key.trim()] = item.value.trim();
                }

                if (typeof configuration.uriMapper !== "undefined") {
                    var mapper = Bifrost.uriMappers[configuration.uriMapper];
                    var frame = Bifrost.navigation.NavigationFrame.create({
                        stringMapper: mapper,
                        home: configuration.home || ''
                    });
                    frame.setContainer(element);

                    element.navigationFrame = frame;
                }
            });
        };
    })
});
if (typeof ko !== 'undefined' && typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
    ko.bindingHandlers.navigateTo = {
        init: function (element, valueAccessor, allBindingAccessor, viewModel) {
            ko.applyBindingsToNode(element, { 
				click: function() {
					var featureName = valueAccessor()();
					History.pushState({feature:featureName},$(element).attr("title"),"/"+featureName);
				} 
			}, viewModel);
        }
    };
}
Bifrost.namespace("Bifrost.navigation", {
    navigateTo: function (featureName, queryString) {
        var url = featureName;

        if (featureName.charAt(0) !== "/")
            url = "/" + url;
        if (queryString)
            url += queryString;

        // TODO: Support title somehow
        if (typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
            History.pushState({}, "", url);
        }
    },
    navigationManager: {
        hookup: function () {
            if (typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
                $("body").click(function (e) {
                    var href = e.target.href;
                    if (typeof href == "undefined") {
                        var closestAnchor = $(e.target).closest("a")[0];
                        if (!closestAnchor) {
                            return;
                        }
                        href = closestAnchor.href;
                    }
                    if (href.indexOf("#") > 0) {
                        href = href.substr(0, href.indexOf("#"));
                    }

                    if (href.length == 0) {
                        href = "/";
                    }
                    var targetUri = Bifrost.Uri.create(href);
                    if (targetUri.isSameAsOrigin &&
                        targetUri.queryString.indexOf("postback")<0) {
                        var target = targetUri.path;
                        while (target.indexOf("/") == 0) {
                            target = target.substr(1);
                        }
                        e.preventDefault();

                        var result = $(e.target).closest("[data-navigation-target]");
                        if (result.length == 1) {
                            var id = $(result[0]).data("navigation-target");
                            var element = $("#"+id);
                            if (element.length == 1 && typeof element[0].navigationFrame !== "undefined") {
                                element[0].navigationFrame.navigate(targetUri.path);
                            } else {
                                // Element not found
                            }
                        } else {
                            var queryString = targetUri.queryString.length > 0 ? "?" + targetUri.queryString : "";
                            History.pushState({}, "", "/" + target + queryString);
                        }
                    }
                });
            }
        }
    }
});
Bifrost.namespace("Bifrost.navigation", {
    NavigationFrameViewRenderer: Bifrost.views.ViewRenderer.extend(function () {

        this.canRender = function (element) {
            return typeof $(element).data("navigation-frame") !== "undefined" && 
                    typeof $(element).data("view") === "undefined";
        };

        this.render = function (element) {
            var promise = Bifrost.execution.Promise.create();

            var configurationString = $(element).data("navigation-frame");
            var configurationItems = ko.expressionRewriting.parseObjectLiteral(configurationString);

            var configuration = {};

            for (var index = 0; index < configurationItems.length; index++) {
                var item = configurationItems[index];
                configuration[item.key.trim()] = item.value.trim();
            }

            if (typeof configuration.uriMapper !== "undefined") {
                $(element).data("urimapper", configuration.uriMapper);
            } else {
                configuration.uriMapper = "default";
            }

            var frame = Bifrost.navigation.NavigationFrame.create({
                home: configuration.home || '',
                locationAware: configuration.locationAware || true,
                uriMapper: Bifrost.uriMappers[configuration.uriMapper]
            });
            element.navigationFrame = frame;
            frame.setContainer(element).continueWith(function (view) {
                promise.signal(view);
            });

            return promise;
        };
    })
});
if (typeof Bifrost.views.viewRenderers != "undefined") {
    Bifrost.views.viewRenderers.NavigationFrameViewRenderer = Bifrost.navigation.NavigationFrameViewRenderer;
}
if (typeof ko !== "undefined") {
    (function () {
        var historyEnabled = typeof History !== "undefined" && typeof History.Adapter !== "undefined";

        ko.observableQueryParameter = function (parameterName, defaultValue) {
            var self = this;
            var observable = null;

            this.getState = function () {
                var uri = Bifrost.Uri.create(location.toString());
                if (uri.parameters.hasOwnProperty(parameterName)) {
                    return uri.parameters[parameterName];
                }

                return null;
            }

            if (historyEnabled) {
                History.Adapter.bind(window, "statechange", function () {
                    if (observable != null) {
                        observable(self.getState());
                    }
                });
            }

            observable = ko.observable(self.getState() || defaultValue);

            observable.subscribe(function (newValue) {
                var state = History.getState();
                state[parameterName] = newValue;

                var parameters = Bifrost.hashString.decode(state.url);
                parameters[parameterName] = newValue;


                var url = "?";
                var parameterIndex = 0;
                for (var parameter in parameters) {
                    if (parameterIndex > 0) {
                        url += "&";
                    }
                    url += parameter + "=" + parameters[parameter];
                    parameterIndex++;
                }

                if (historyEnabled) {
                    History.pushState(state, state.title, url);
                }
            });

            return observable;
        }
    })();
}
Bifrost.namespace("Bifrost", {
    configure: (function () {
        var self = this;

        this.ready = false;
        this.readyCallbacks = [];

        function ready(callback) {
            if (self.ready == true) {
                callback();
            } else {
                readyCallbacks.push(callback);
            }
        }

        function onReady() {
            self.ready = true;
            for (var callbackIndex = 0; callbackIndex < self.readyCallbacks.length; callbackIndex++) {
                self.readyCallbacks[callbackIndex]();
            }
        }

        function onStartup() {
            var self = this;

            Bifrost.dependencyResolvers.DOMRootDependencyResolver.documentIsReady();
            Bifrost.views.viewModelBindingHandler.initialize();
            Bifrost.views.viewBindingHandler.initialize();

            if (typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
                Bifrost.WellKnownTypesDependencyResolver.types.history = History;
            }

            var defaultUriMapper = Bifrost.StringMapper.create();
            defaultUriMapper.addMapping("{boundedContext}/{module}/{feature}/{view}", "/{boundedContext}/{module}/{feature}/{view}.html");
            defaultUriMapper.addMapping("{boundedContext}/{feature}/{view}", "/{boundedContext}/{feature}/{view}.html");
            defaultUriMapper.addMapping("{feature}/{view}", "/{feature}/{view}.html");
            defaultUriMapper.addMapping("{view}", "{view}.html");
            Bifrost.uriMappers.default = defaultUriMapper;

            var bifrostVisualizerUriMapper = Bifrost.StringMapper.create();
            bifrostVisualizerUriMapper.addMapping("Visualizer/{module}/{view}", "/Bifrost/Visualizer/{module}/{view}.html");
            bifrostVisualizerUriMapper.addMapping("Visualizer/{view}", "/Bifrost/Visualizer/{view}.html");
            Bifrost.uriMappers.bifrostVisualizer = bifrostVisualizerUriMapper;

            var promise = Bifrost.assetsManager.initialize();
            promise.continueWith(function () {
                self.onReady();
                Bifrost.views.viewManager.create().initializeLandingPage();
                Bifrost.navigation.navigationManager.hookup();
            });
        }

        function reset() {
            self.ready = false;
            self.readyCallbacks = [];
        }

        return {
            ready: ready,
            onReady: onReady,
            onStartup: onStartup,
            reset: reset,
            isReady: function () {
                return self.ready;
            }
        }
    })()
});
(function ($) {
    $(function () {
        if( typeof Bifrost.assetsManager !== "undefined" ) {
            Bifrost.configure.onStartup();
        }
    });
})(jQuery);
