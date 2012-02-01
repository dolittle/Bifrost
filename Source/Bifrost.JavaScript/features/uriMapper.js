Bifrost.namespace("Bifrost.features");
Bifrost.features.uriMapper = (function () {
    var mappings = new Array();

    return {
        clear: function () {
            mappings = new Array();
        },

        add: function (uri, mappedUri) {
            var uriMapping = Bifrost.features.UriMapping.create(uri, mappedUri);
            mappings.push(uriMapping);
        },

        resolve: function (uri) {
            var found;
            $.each(mappings, function (i, m) {
                if (m.matches(uri)) {
                    found = m;
                    return;
                }
            });

            if (typeof found !== "undefined") {
                return found.resolve(uri);
            }
            return "";
        },

        allMappings: function () {
            var allMappings = new Array();
            allMappings = allMappings.concat(mappings);
            return allMappings;
        }
    }
})();