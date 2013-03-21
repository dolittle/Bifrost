describe("when matching a uri with wildcard at the end", function () {
    var uriMapping = Bifrost.navigation.UriMapping.create({
        uri: "{something}/**/",
        mappedUri: "whatevva.**."
    });
    var result = uriMapping.matches("this/is/a/wildcard/uri");

    it("should match", function () {
        expect(result).toBe(true);
    });
});