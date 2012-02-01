describe("when matching a uri that should match", function () {
    var uriMapping = Bifrost.features.UriMapping.create("{something}/{else}", "whatevva");
    var result = uriMapping.matches("hello/there");

    it("should not match", function () {
        expect(result).toBe(true);
    });
});