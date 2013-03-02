Bifrost.namespace("Bifrost.navigation", {
    UriMapper: Bifrost.Type.extend(function () {
        var self = this;

        this.mappings = [];

        this.addMapping = function (uri, mappedUri) {
            var mapping = Bifrost.navigation.UriMapping.create({
                uri: uri,
                mappedUri: mappedUri
            });
            self.mappings.push(mapping);
        };
    })
});