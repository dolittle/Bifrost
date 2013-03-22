Bifrost.namespace("Bifrost.navigation", {
    UriMapping: Bifrost.Type.extend(function (uri, mappedUri) {
        var self = this;

        this.uri = uri;
        this.mappedUri = mappedUri;

        var placeholderExpression = "\{[a-zA-Z]+\}";
        var placeholderRegex = new RegExp(placeholderExpression, "g");

        var wildcardExpression = "\\*{2}[//||\.]";
        var wildcardRegex = new RegExp(wildcardExpression, "g");

        var combinedExpression = "(" + placeholderExpression + ")*(" + wildcardExpression + ")*";
        var combinedRegex = new RegExp(combinedExpression, "g");

        var components = [];
        

        var resolveExpression = uri.replace(combinedRegex, function(match) {
            if( typeof match === "undefined" || match == "") return "";
            components.push(match);
            if( match.indexOf("**") == 0) return "([\\w.//]*)";
            return "([\\w.]*)";
        });

        var mappedUriWildcardMatch = mappedUri.match(wildcardRegex);
        var uriRegex = new RegExp(resolveExpression);

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
            var wildcardOffset = 0;

            $.each(components, function (i, c) {
                var value = match[i + 1];
                if( c.indexOf("**") == 0 ) {
                    var wildcard = mappedUriWildcardMatch[wildcardOffset];
                    value = value.replaceAll(c[2],wildcard[2]);
                    result = result.replace(wildcard, value);
                    wildcardOffset++;
                } else {
                    result = result.replace(c, value);
                }
            });

            return result;
        }
    })
});