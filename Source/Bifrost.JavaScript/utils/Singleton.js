Bifrost.namespace("Bifrost", {
    Singleton: function (typeDefinition) {
        return Bifrost.Type.extend(typeDefinition).scopeTo(window);
        /*
        var identifier = Bifrost.Guid.create();
        var type = Bifrost.Type.extend(typeDefinition);
        window._singletons = window._singletons || {};
        return type.scopeTo(function (namespace, name) {
            
        var exists = typeof window._singletons[identifier] !== "undefined";
        window._singletons[identifier] = true;
        return window;
        });*/
    }
});