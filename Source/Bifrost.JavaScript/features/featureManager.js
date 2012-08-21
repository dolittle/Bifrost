Bifrost.namespace("Bifrost.features");
Bifrost.features.featureManager = (function () {
    var allFeatures = {};

    return {
        get: function (name) {
            name = name.toLowerCase();

            if (typeof allFeatures[name] !== "undefined") {
                return allFeatures[name];
            }

            var FeatureMapping = Bifrost.features.featureMapper.getFeatureMappingFor(name);
            var path = FeatureMapping.resolve(name);
            var feature = Bifrost.features.Feature.create(name, path, FeatureMapping.isDefault);
            allFeatures[name] = feature;
            feature.load();            
            return feature;
        },
        hookup: function ($) {
            $("[data-feature]").each(function () {
                var target = $(this)[0];
                var name = $(this).attr("data-feature");
                var feature = Bifrost.features.featureManager.get(name);
                feature.renderTo(target);
            });
        },
        all: function () {
            return allFeatures;
        }
    }
})();