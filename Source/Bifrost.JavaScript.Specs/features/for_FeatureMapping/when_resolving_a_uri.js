describe("when resolving a uri", function () {
    var expectedResult = "Say/hello/to/mr.potatohead";
    var uriMapping = Bifrost.features.FeatureMapping.create("{something}/{else}", "Say/{else}/to/{something}");
    var result = uriMapping.resolve("mr.potatohead/hello");

    it("should expand input uri to the mapped uri", function () {
        expect(result).toBe(expectedResult);
    });
});