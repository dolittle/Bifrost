Bifrost.namespace("Bifrost", {
    assetsManager: {
        scripts: [],
        isInitialized: function() {
            return Bifrost.assetsManager.scripts.length > 0;
        },
        initialize: function () {
            var promise = Bifrost.execution.Promise.create();
            if (!Bifrost.assetsManager.isInitialized()) {
                $.get("/Bifrost/AssetsManager", { extension: "js" }, function (result) {
                    Bifrost.assetsManager.initializeFromAssets(result);
                    promise.signal();
                }, "json");
            } else {
                promise.signal();
            }
            return promise;
        },
        initializeFromAssets: function (assets) {
            if (!Bifrost.assetsManager.isInitialized()) {
                Bifrost.assetsManager.scripts = assets;
                Bifrost.namespaces.create().initialize();
            }
        },
        getScripts: function () {
            return Bifrost.assetsManager.scripts;
        },
        hasScript: function(script) {
            return Bifrost.assetsManager.scripts.some(function (scriptInSystem) {
                return scriptInSystem === script;
            });
        },
        getScriptPaths: function () {
            var paths = [];

            Bifrost.assetsManager.scripts.forEach(function (fullPath) {
                var path = Bifrost.Path.getPathWithoutFilename(fullPath);
                if (paths.indexOf(path) === -1) {
                    paths.push(path);
                }
            });
            return paths;
        }
    }
});