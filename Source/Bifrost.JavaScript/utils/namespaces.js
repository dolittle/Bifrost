Bifrost.namespace("Bifrost", {
    namespaces: Bifrost.Singleton(function() {
        var self = this;

        this.stripPath = function (path) {
            if (path.startsWith("/")) {
                path = path.substr(1);
            }
            if (path.endsWith("/")) {
                path = path.substr(0, path.length - 1);
            }
            return path;
        };

        this.initialize = function () {
            var scripts = Bifrost.assetsManager.getScripts();
            if (typeof scripts === "undefined") return;

            $.each(scripts, function (index, fullPath) {
                var path = Bifrost.path.getPathWithoutFilename(fullPath);
                path = self.stripPath(path);

                for( var mapper in Bifrost.

                $.each(self.conventions, function (conventionIndex, convention) {
                    if (path.startsWith(convention.path)) {
                        var namespacePath = path.substr(convention.path.length);
                        namespacePath = self.stripPath(namespacePath);
                        namespacePath = namespacePath.split("/").join(".");
                        if (convention.namespace.length > 0) {
                            namespacePath = convention.namespace + ((namespacePath.length > 0) ? "."+namespacePath:"");
                        }
                        var namespace = Bifrost.namespace(namespacePath);
                        var root = "/" + path + "/";
                        namespace._path = root;

                        if (typeof namespace._scripts === "undefined") {
                            namespace._scripts = [];
                        }

                        var fileIndex = fullPath.lastIndexOf("/");
                        var file = fullPath.substr(fileIndex + 1);
                        var extensionIndex = file.lastIndexOf(".");
                        var system = file.substr(0, extensionIndex);

                        namespace._scripts.push(system);
                    }
                });
            });
        };
    })
});