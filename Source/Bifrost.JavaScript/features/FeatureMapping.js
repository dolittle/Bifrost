Bifrost.namespace("Bifrost.features");
Bifrost.features.FeatureMapping = (function () {
    function throwIfNotString(input, message) {
        if( typeof input !== "string" ) {
            throw {
                name: "ArgumentError",
                message: message
            }
        }
    }

    function FeatureMapping(uri, mappedUri, isDefault) {
        throwIfNotString(uri, "Missing uri for FeatureMapping");
        throwIfNotString(mappedUri, "Missing mappedUri for FeatureMapping");

        var uriComponentRegex = /\{[a-zA-Z]*\}/g
        var components = uri.match(uriComponentRegex) || [];
        var uriRegex = new RegExp(uri.replace(uriComponentRegex, "([\\w.]*)"));

        this.uri = uri;
        this.mappedUri = mappedUri;
        this.isDefault = isDefault || false;

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
    }

    return {
        create: function (uri, mappedUri, isDefault) {
            var FeatureMapping = new FeatureMapping(uri, mappedUri, isDefault);
            return FeatureMapping;
        }
    }
})();
