describe("when checking if has script and it has", function () {
    var result = false;
    beforeEach(function () {
        Bifrost.assetsManager.scripts = ["something.js", "thestuff.js"];
        result = Bifrost.assetsManager.hasScript("thestuff.js");
    });

    afterEach(function () {
        Bifrost.assetsManager.scripts = [];
    });

    it("should return that it has it", function () {
        expect(result).toBe(true);
    });
});