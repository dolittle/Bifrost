describe("when initializing", function() {
    var extension = "";

    beforeEach(function () {
        sinon.stub($, "get", function (url, parameters, callback) {
            extension = parameters.extension;
        });

        Bifrost.assetsManager.initialize();
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


});