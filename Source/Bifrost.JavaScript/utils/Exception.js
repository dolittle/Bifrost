Bifrost.namespace("Bifrost");

Bifrost.DefinitionMustBeFunction = function (message) {
    this.prototype = Error.prototype;
    this.name = "DefinitionMustBeFunction";
    this.message = message || "Definition must be function";
};

Bifrost.MissingName = function (message) {
    this.prototype = Error.prototype;
    this.name = "MissingName";
    this.message = message || "Missing name";
};

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
            
            var exception = function (message) {
                this.name = exceptionName;
                this.message = message || defaultMessage;
            };
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