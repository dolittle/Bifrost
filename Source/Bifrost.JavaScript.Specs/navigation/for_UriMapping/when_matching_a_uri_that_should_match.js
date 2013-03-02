describe("when matching a uri that should match", function () {
    var FeatureMapping = Bifrost.navigation.UriMapping.create({
        uri: "{something}/{else}",
        mappedUri: "whatevva"
    });
    var result = FeatureMapping.matches("hello/there");

    it("should not match", function () {
        expect(result).toBe(true);
    });
});