Bifrost.namespace("Bifrost", {
    assetsManager: {
        initialize: function () {
            $.get("/AssetsManager", { extension: "js" }, function (result) {
                Bifrost.assetsManager.scripts = result;
            }, "json");
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