Bifrost.namespace("Bifrost.navigation", {
    UriMapper: Bifrost.Type.extend(function () {
        var self = this;

        this.mappings = [];


        this.getFeatureMappingFor = function (uri) {
            var found;
            $.each(self.mappings, function (i, m) {
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
        };

        this.resolve = function (uri) {
            try {
                if( uri === null || typeof uri === "undefined" || uri === "" ) return "";
                
                var mapping = self.getFeatureMappingFor(uri);
                return mapping.resolve(uri);
            } catch (e) {
                return "";
            }
        };

        this.addMapping = function (uri, mappedUri) {
            var mapping = Bifrost.navigation.UriMapping.create({
                uri: uri,
                mappedUri: mappedUri
            });
            self.mappings.push(mapping);
        };
    })
});