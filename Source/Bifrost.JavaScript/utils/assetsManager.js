Bifrost.namespace("Bifrost", {
    assetsManager: {
        initialize: function () {
            var promise = Bifrost.execution.Promise.create();
            $.get("/AssetsManager", { extension: "js" }, function (result) {
                Bifrost.assetsManager.scripts = result;
                Bifrost.namespaces.initialize();
                promise.signal();
            }, "json");
            return promise;
        },
        getScripts: function () {
            return Bifrost.assetsManager.scripts;
        },
        getScriptPaths: function () {
            var paths = [];

            $.each(Bifrost.assetsManager.scripts, function (index, fullPath) {
                var path = Bifrost.path.getPathWithoutFilename(fullPath);
                if (paths.indexOf(path) == -1) {
                    paths.push(path);
                }
            });
            return paths;
        }
    }
});