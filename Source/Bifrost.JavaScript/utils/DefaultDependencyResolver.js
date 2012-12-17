Bifrost.namespace("Bifrost", {
    DefaultDependencyResolver: function () {
        var self = this;

        this.doesNamespaceHave = function(namespace, name) {
            return namespace.hasOwnProperty(name);
        };

        this.doesNamespaceHaveScriptReference = function(namespace, name) {
            if( namespace.hasOwnProperty("_scripts") && Bifrost.isArray(namespace._scripts)) {
                for( var i=0; i<namespace._scripts.length; i++ ) {
                    var script = namespace._scripts[i];
                    if( script === name ) {
                        return true;
                    }
                }
            }
            return false;
        };

        this.getFileName = function(namespace, name) {
            var fileName = "";
            if( typeof namespace._path !== "undefined" ) {
                fileName += namespace._path;
                if( !fileName.endsWith("/") ) {
                    fileName += "/";
                }
            } 
            fileName += name;
            if( !fileName.endsWith(".js") ) {
                fileName += ".js";
            }
            return fileName;

        };

        this.loadScriptReference = function(namespace, name, promise) {
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
                if (self.doesNamespaceHaveScriptReference(current,name)) {
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
                if (self.doesNamespaceHave(current,name)) {
                    return current[name];
                }
                if (self.doesNamespaceHaveScriptReference(current,name)) {
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
