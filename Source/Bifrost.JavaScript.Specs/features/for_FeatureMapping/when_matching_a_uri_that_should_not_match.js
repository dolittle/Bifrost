describe("when matching a uri that should not match", function () {
    var FeatureMapping = Bifrost.features.FeatureMapping.create("{something}/{else}", "whatevva");
    var result = FeatureMapping.matches("hello");

    it("should not match", function () {
        expect(result).toBe(false);
    });
});