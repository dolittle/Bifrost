describe("when asking for view model for view", function () {
    var scriptAskedFor = null;

    var viewModelManager = Bifrost.views.viewModelManager.create({
        assetsManager: {
            hasScript: function (script) {
                scriptAskedFor = script;
            }
        },
        documentService: {},
        viewModelLoader: {}
    });

    viewModelManager.hasForView("/Something/index.html");

    it("should ask the assets manager if it has the script", function () {
        expect(scriptAskedFor).toBe("Something/index.js");
    });
});