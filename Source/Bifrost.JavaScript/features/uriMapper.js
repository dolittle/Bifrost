Bifrost.namespace("Bifrost.features");
Bifrost.features.uriMapper = (function () {
    var mappings = new Array();

    return {
        add: function (uri, mappedUri) {
            var uriMapping = Bifrost.features.UriMapping.create(uri, mappedUri);
            mappings.push(uriMapping);

        },

        resolve: function (input) {
        },

        allMappings: function () {
            var allMappings = new Array();
            allMappings = allMappings.concat(mappings);
            return allMappings;
        }
    }
})();