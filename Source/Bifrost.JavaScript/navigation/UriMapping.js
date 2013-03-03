Bifrost.namespace("Bifrost.navigation", {
    UriMapping: Bifrost.Type.extend(function (uri, mappedUri) {
        var self = this;

        this.uri = uri;
        this.mappedUri = mappedUri;

        var uriComponentRegex = /\{[a-zA-Z]*\}/g
        var components = uri.match(uriComponentRegex) || [];
        var uriRegex = new RegExp(uri.replace(uriComponentRegex, "([\\w.]*)"));

        this.uri = uri;
        this.mappedUri = mappedUri;

        this.matches = function (uri) {
            var match = uri.match(uriRegex);
            if (match) {
                return true;
            }
            return false;
        }

        this.resolve = function (uri) {
            var match = uri.match(uriRegex);
            var result = mappedUri;
            $.each(components, function (i, c) {
                result = result.replace(c, match[i + 1]);
            });

            return result;
        }
    })
});