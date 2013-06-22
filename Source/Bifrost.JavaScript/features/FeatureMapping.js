Bifrost.namespace("Bifrost.features");
Bifrost.features.FeatureMapping = (function () {
	function throwIfUriNotSpecified(uri) {
		if(!uri || typeof uri === "undefined") {
			throw new Bifrost.features.UriNotSpecified();
		}
	}
	
	function throwIfMappedUriNotSpecified(mappedUri) {
		if(!mappedUri || typeof mappedUri === "undefined") {
			throw new Bifrost.features.MappedUriNotSpecified();
		}
	}
	
    function FeatureMapping(uri, mappedUri, isDefault) {

		throwIfUriNotSpecified(uri);
		throwIfMappedUriNotSpecified(mappedUri);

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
            components.forEach(function (c, i) {
                result = result.replace(c, match[i + 1]);
            });

            return result;
        }
    }

    return {
        create: function (uri, mappedUri, isDefault) {
            var featureMapping = new FeatureMapping(uri, mappedUri, isDefault);
            return featureMapping;
        }
    }
})();
