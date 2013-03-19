describe("when checking if has script and it has not", function () {
    var result = false;
    beforeEach(function () {
        Bifrost.assetsManager.scripts = ["something.js", "thestuff.js"];
        result = Bifrost.assetsManager.hasScript("missing.js");
    });

    afterEach(function () {
        Bifrost.assetsManager.scripts = undefined;
    });

    it("should return that it has it", function () {
        expect(result).toBe(false);
    });
});