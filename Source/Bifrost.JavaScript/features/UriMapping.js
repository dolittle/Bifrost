Bifrost.namespace("Bifrost.features");
Bifrost.features.UriMapping = (function () {
    function UriMapping(uri, mappedUri) {
        this.uri = uri;
        this.mappedUri = mappedUri;
    }

    return {
        create: function (uri, mappedUri) {
            var uriMapping = new UriMapping(uri, mappedUri);
            return uriMapping;
        }
    }
})();