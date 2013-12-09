Bifrost.namespace("Bifrost", {
    assetsManager: {
        initialize: function () {
            var promise = Bifrost.execution.Promise.create();
            if (typeof Bifrost.assetsManager.scripts === "undefined" ||
                Bifrost.assetsManager.scripts.length == 0) {

                $.get("/Bifrost/AssetsManager", { extension: "js" }, function (result) {
                    Bifrost.assetsManager.scripts = result;
                    Bifrost.namespaces.create().initialize();
                    promise.signal();
                }, "json");
            } else {
                promise.signal();
            }
            return promise;
        },
        initializeFromAssets: function(assets) {
            Bifrost.assetsManager.scripts = assets;
            Bifrost.namespaces.create().initialize();
        },
        getScripts: function () {
            return Bifrost.assetsManager.scripts;
        },
        hasScript: function(script) {
            var found = false;
            Bifrost.assetsManager.scripts.some(function (scriptInSystem) {
                if (scriptInSystem === script) {
                    found = true;
                    return;
                }
            });

            return found;
        },
        getScriptPaths: function () {
            var paths = [];

            Bifrost.assetsManager.scripts.forEach(function (fullPath) {
                var path = Bifrost.Path.getPathWithoutFilename(fullPath);
                if (paths.indexOf(path) == -1) {
                    paths.push(path);
                }
            });
            return paths;
        }
    }
});
//Bifrost.WellKnownTypesDependencyResolver.types.assetsManager = Bifrost.commands.commandCoordinator;