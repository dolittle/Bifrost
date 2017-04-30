Bifrost.namespace("Bifrost.io", {
    fileManager: Bifrost.Singleton(function () {
        /// <summary>Represents a manager for files, providing capabilities of loading and more</summary>
		var self = this;

		var scriptSource = (function () {
			var scripts = document.getElementsByTagName('script'),
				script = scripts[scripts.length - 1];

			if (script.getAttribute.length !== undefined) {
				return script.src;
			}

			return script.getAttribute('src', -1);
		}());

		var uri = Bifrost.Uri.create(scriptSource);
        if (window.location.protocol === "file:") {
            this.origin = window.location.href;
            this.origin = this.origin.substr(0, this.origin.lastIndexOf("/"));

            if (this.origin.lastIndexOf("/") === this.origin.length - 1) {
                this.origin = this.origin.substr(0, this.origin.length - 1);
            }
        } else {
            var port = uri.port || "";
            if (!Bifrost.isUndefined(port) && port !== "" && port !== 80) {
                port = ":" + port;
            }

            this.origin = uri.scheme + "://" + uri.host + port;
        }

        function getActualFilename(filename) {
            var actualFilename = self.origin;

            if (filename.indexOf("/") !== 0) {
                actualFilename += "/";
            }
            actualFilename += filename;

            return actualFilename;
        }

        this.load = function (files) {
            /// <summary>Load files</summary>
            /// <param parameterArray="true" elementType="Bifrost.io.File">Files to load</param>
            /// <returns type="Bifrost.execution.Promise">A promise that can be continued with the actual files coming in as an array</returns>
            var filesToLoad = [];

            var promise = Bifrost.execution.Promise.create();

            files.forEach(function (file) {
                var path = getActualFilename(file.path.fullPath);
                if (file.fileType === Bifrost.io.fileType.html) {
                    path = "text!" + path + "!strip";
                    if (!file.path.hasExtension()) {
                        path = "noext!" + path;
                    }
                }

                filesToLoad.push(path);
            });

            require(filesToLoad, function () {
                promise.signal(arguments);
            });

            return promise;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.fileManager = Bifrost.io.fileManager;