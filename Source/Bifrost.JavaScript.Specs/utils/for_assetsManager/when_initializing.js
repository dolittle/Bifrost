describe("when initializing", function () {
    var extension = "";

    beforeEach(function () {
        Bifrost.namespaces = Bifrost.namespaces || {};
        Bifrost.namespaces.initialize = sinon.stub();
        sinon.stub($, "get", function (url, parameters, callback) {
            extension = parameters.extension;
        });

        Bifrost.assetsManager.initialize();
    });

    afterEach(function () {
        $.get.restore();
        Bifrost.assetsManager.scripts = [];
    });

    it("should call server to get assets", function () {
        expect($.get.called).toBe(true);
    });

    it("should get files with js extension", function () {
        expect(extension).toBe("js");
    });
});