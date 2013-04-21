Bifrost.namespace("Bifrost", {
    Singleton: function (typeDefinition) {
        return Bifrost.Type.extend(typeDefinition).scopeTo(window);
    }
});