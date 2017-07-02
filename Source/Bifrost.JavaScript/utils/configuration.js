Bifrost.namespace("Bifrost", {
    configuration: Bifrost.Singleton(function () {
        var self = this;

        function getOriginFrom(uri) {
            var origin = "";
            if (window.location.protocol === "file:") {
                origin = window.location.href;
                origin = origin.substr(0, origin.lastIndexOf("/"));

                if (origin.lastIndexOf("/") === origin.length - 1) {
                    origin = origin.substr(0, origin.length - 1);
                }
            } else {
                var port = uri.port || "";
                if (!Bifrost.isUndefined(port) && port !== "" && port !== 80) {
                    port = ":" + port;
                }

                origin = uri.scheme + "://" + uri.host + port;
            }
            return origin;
        }

        function getOriginFromElement(dataAttributeName) {
            var element = null;
            if (document.body.attributes[dataAttributeName]) element = document.body;
            else element = document.querySelector("["+dataAttributeName+"]");
            var originUri = null;
            if (element !== null) {
                var origin = element.attributes[dataAttributeName].value
                if (origin == "" && velement.localName == "script") originUri = Bifrost.Uri.create(element.src);
                else originUri = Bifrost.Uri.create(origin);
            }
            return originUri;
        }

        this.origins = {
            files: null,
            APIs: null
        };

        Bifrost.configure.ready(function () {
            var currentScript = document.currentScript || (function () {
                var scripts = document.getElementsByTagName('script');
                return scripts[scripts.length - 1];
            })();
            var currentScriptUri = Bifrost.Uri.create(currentScript.src);
            var filesOriginUri = getOriginFromElement("data-files-origin") || currentScriptUri;
            var APIsOriginUri = getOriginFromElement("data-apis-origin") || currentScriptUri;

            if (!self.origins.files) self.origins.files = getOriginFrom(filesOriginUri);
            if (!self.origins.APIs) self.origins.APIs = getOriginFrom(APIsOriginUri);
        });
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.configuration = Bifrost.configuration.create();