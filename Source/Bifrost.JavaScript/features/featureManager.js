Bifrost.namespace("Bifrost.features");
Bifrost.features.featureManager = (function () {
    var allFeatures = {};

    return {
        get: function (name) {
            name = name.toLowerCase();

            if (typeof allFeatures[name] !== "undefined") {
                return allFeatures[name];
            }

            var uriMapping = Bifrost.features.uriMapper.getUriMappingFor(name);
            var path = uriMapping.resolve(name);
            var feature = Bifrost.features.Feature.create(name, path, uriMapping.isDefault);
            allFeatures[name] = feature;
            return feature;
        },
        all: function () {
            return allFeatures;
        }
    }
})();