describe("when resolving a string", function () {
    var expectedResult = "Say/hello/to/mr.potatohead";
    var mapping = Bifrost.StringMapping.create({
        format: "{something}/{else}",
        mappedFormat: "Say/{else}/to/{something}"
    });
    var result = mapping.resolve("mr.potatohead/hello");

    it("should expand input string to the mapped string", function () {
        expect(result).toBe(expectedResult);
    });
});