Bifrost.namespace("Bifrost.navigation", {
    UriMapping: Bifrost.Type.extend(function (uri, mappedUri) {
        var self = this;

        this.uri = uri;
        this.mappedUri = mappedUri;

        var placeholderExpression = "\\{[a-zA-Z]*\\}";
        var placeholderRegex = new RegExp(placeholderExpression, "g");

        var wildcardExpression = "\\*{2}[//||\.]";
        var wildcardRegex = new RegExp(wildcardExpression, "g");

        var uriComponentExpression = "(" + placeholderExpression + ")*(" + wildcardExpression + ")*";
        var uriComponentRegex = new RegExp(uriComponentExpression, "g");

        //placeholderRegex = /\{[a-zA-Z]*\}/g
            

        var components = uri.match(uriComponentRegex) || [];

        print("Components : " + components);

        //var uriExpression = uri.replace(wildcardRegex, "([\\w.]*[//||\.])*")
        var uriExpression = uri.replace(placeholderRegex, "([\\w.]*)")
        //print("Uri Expression : " + uriExpression+" : "+components);

        var uriRegex = new RegExp(uriExpression, "g");

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