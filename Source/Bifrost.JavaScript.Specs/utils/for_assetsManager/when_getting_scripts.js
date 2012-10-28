describe("when getting scripts", function () {
    var extension = "";
    var scripts = ["something.js", "something_else.js"];
    var scriptsReturned = [];

    beforeEach(function () {
        sinon.stub($, "get", function (url, parameters, callback) {
            extension = parameters.extension;
            callback(scripts);
        });

        Bifrost.assetsManager.getScripts(function (result) {
            scriptsReturned = result;
        });
    });

    afterEach(function () {
        $.get.restore();
    });

    it("should call server to get assets", function () {
        expect($.get.called).toBe(true);
    });

    it("should get files with js extension", function () {
        expect(extension).toBe("js");
    });

    it("should call the callback with the scripts", function () {
        expect(scriptsReturned).toBe(scripts);
    });
});