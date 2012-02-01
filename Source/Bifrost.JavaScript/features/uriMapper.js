Bifrost.namespace("Bifrost.features");
Bifrost.features.uriMapper = (function () {
    var mappings = new Array();

    return {
        clear: function () {
            mappings = new Array();
        },

        add: function (uri, mappedUri, isDefault) {
            var uriMapping = Bifrost.features.UriMapping.create(uri, mappedUri, isDefault);
            mappings.push(uriMapping);
        },

        getUriMappingFor: function (uri) {
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
                var uriMapping = Bifrost.features.uriMapper.getUriMappingFor(uri);
                return uriMapping.resolve(uri);
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