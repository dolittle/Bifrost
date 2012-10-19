Bifrost.namespace("Bifrost", {
    assetsManager: {
        getScripts: function (callback) {
            $.get("/AssetsManager", { extension: "js" }, function (result) {
                callback(result);
            }, "json");
        }
    }
});