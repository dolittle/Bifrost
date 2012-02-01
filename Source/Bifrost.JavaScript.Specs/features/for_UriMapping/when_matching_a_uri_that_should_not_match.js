describe("when matching a uri that should not match", function () {
    var uriMapping = Bifrost.features.UriMapping.create("{something}/{else}", "whatevva");
    var result = uriMapping.matches("hello");

    it("should not match", function () {
        expect(result).toBe(false);
    });
});