describe("when getting scripts", function () {
    var extension = "";
    var scripts = ["something.js", "something_else.js"];
    var scriptsReturned = [];
    var promiseCalled = false;

    beforeEach(function () {
        Bifrost.assetsManager.scripts = undefined;
        Bifrost.namespaces = Bifrost.namespaces || {};
        Bifrost.namespaces.initialize = sinon.stub();
        sinon.stub($, "get", function (url, parameters, callback) {
            extension = parameters.extension;
            callback(scripts);
        });

        Bifrost.assetsManager.initialize().continueWith(function () {
            promiseCalled = true;
        });

        scriptsReturned = Bifrost.assetsManager.getScripts();
    });

    afterEach(function () {
        $.get.restore();
        Bifrost.assetsManager.scripts = undefined;
    });

    it("should get scripts", function () {
        expect(scriptsReturned).toBe(scripts);
    });

    it("should initialize namespaces after scripts have been received", function () {
        expect(Bifrost.namespaces.initialize.called).toBe(true);
    });

    it("should signal the promise after scripts have been received", function () {
        expect(promiseCalled).toBe(true);
    });
});