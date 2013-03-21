describe("when resolving a uri with wildcard at the end", function () {
    var expectedResult = "this.is.a.wildcard.uri";
    var uriMapping = Bifrost.navigation.UriMapping.create({
        uri: "{something}/**/",
        mappedUri: "{something}.**."
    });
    var result = uriMapping.resolve("this/is/a/wildcard/uri");

    it("should expand input uri to the mapped uri", function () {
        expect(result).toBe(expectedResult);
    });
});