Bifrost.namespace("Bifrost.features");
Bifrost.features.featureMapper = (function () {
    var mappings = [];

    return {
        clear: function () {
            mappings = [];
        },

        add: function (uri, mappedUri, isDefault) {
            var FeatureMapping = Bifrost.features.FeatureMapping.create(uri, mappedUri, isDefault);
            mappings.push(FeatureMapping);
        },

        getFeatureMappingFor: function (uri) {
            var found;
            $.each(mappings, function (i, m) {
                if (m.matches(uri)) {
                    found = m;
                    return false;
                }
            });

            if (typeof found !== "undefined") {
                return found;
            }

            throw {
                name: "ArgumentError",
                message: "URI (" + uri + ") could not be mapped"
            }
        },

        resolve: function (uri) {
            try {
                var FeatureMapping = Bifrost.features.featureMapper.getFeatureMappingFor(uri);
                return FeatureMapping.resolve(uri);
            } catch (e) {
                return "";
            }
        },

        allMappings: function () {
            var allMappings = new Array();
            allMappings = allMappings.concat(mappings);
            return allMappings;
        }
    }
})();