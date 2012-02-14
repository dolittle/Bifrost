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
        hookup: function ($) {
            $("*[data-feature]").each(function () {
                var target = $(this);
                var name = $(this).attr("data-feature");
                var feature = Bifrost.features.featureManager.get(name);
                feature.renderTo(target[0]);
            });
        },
        all: function () {
            return allFeatures;
        }
    }
})();