describe("when getting scripts", function () {
    var extension = "";
    var scripts = ["something.js", "something_else.js"];
    var scriptsReturned = [];

    beforeEach(function () {
        sinon.stub($, "get", function (url, parameters, callback) {
            extension = parameters.extension;
            callback(scripts);
        });

        Bifrost.assetsManager.initialize();
        scriptsReturned = Bifrost.assetsManager.getScripts();
    });

    afterEach(function () {
        $.get.restore();
    });

    it("should get scripts", function() {
        expect(scriptsReturned).toBe(scripts);
    });
});