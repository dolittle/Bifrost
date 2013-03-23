describe("when resolving a string", function () {
    var expectedResult = "Say/hello/to/mr.potatohead";
    var mapping = Bifrost.utils.StringMapping.create({
        format: "{something}/{else}",
        mappedFormat: "Say/{else}/to/{something}"
    });
    var result = mapping.resolve("mr.potatohead/hello");

    it("should expand input uri to the mapped uri", function () {
        expect(result).toBe(expectedResult);
    });
});