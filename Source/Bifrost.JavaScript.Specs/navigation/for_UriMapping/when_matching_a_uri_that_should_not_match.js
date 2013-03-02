describe("when matching a uri that should not match", function () {
    var FeatureMapping = Bifrost.navigation.UriMapping.create({
        uri: "{something}/{else}",
        mappedUri: "whatevva"
    });
    var result = FeatureMapping.matches("hello");

    it("should not match", function () {
        expect(result).toBe(false);
    });
});