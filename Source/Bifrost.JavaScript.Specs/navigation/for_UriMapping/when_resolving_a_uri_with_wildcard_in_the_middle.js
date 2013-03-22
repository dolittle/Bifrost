
describe("when resolving a uri with wildcard in the middle", function () {
    var expectedResult = "this.is.a.wildcard.uri.for_things";
    var uriMapping = Bifrost.navigation.UriMapping.create({
        uri: "{something}/**/for_{else}",
        mappedUri: "{something}.**.for_{else}"
    });
    var result = uriMapping.resolve("this/is/a/wildcard/uri/for_things");

    it("should expand input uri to the mapped uri", function () {
        expect(result).toBe(expectedResult);
    });
});