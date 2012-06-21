describe("when matching a uri that should match", function () {
    var FeatureMapping = Bifrost.features.FeatureMapping.create("{something}/{else}", "whatevva");
    var result = FeatureMapping.matches("hello/there");

    it("should not match", function () {
        expect(result).toBe(true);
    });
});