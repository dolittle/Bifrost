describe("when resolving a uri", function () {
    var expectedResult = "Say/hello/to/mr.potatohead";
    var uriMapping = Bifrost.navigation.UriMapping.create({
        uri: "{something}/{else}",
        mappedUri: "Say/{else}/to/{something}"
    });
    var result = uriMapping.resolve("mr.potatohead/hello");

    it("should expand input uri to the mapped uri", function () {
        expect(result).toBe(expectedResult);
    });
});