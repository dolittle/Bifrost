Bifrost.namespace("Bifrost", {
    assetsManager: {
        initialize: function () {
            $.get("/AssetsManager", { extension: "js" }, function (result) {
                Bifrost.assetsManager.scripts = result;
            }, "json");
        },
        getScripts: function () {
            return Bifrost.assetsManager.scripts;
        }
    }
});