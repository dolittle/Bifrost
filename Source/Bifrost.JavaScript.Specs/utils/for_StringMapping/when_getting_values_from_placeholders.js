describe("when getting values from placesholders", function () {
    var expectedResult = "Say/hello/to/mr.potatohead";
    var mapping = Bifrost.StringMapping.create({
        format: "{something}/{else}",
        mappedFormat: "Say/{else}/to/{something}"
    });
    var result = mapping.getValues("mr.potatohead/hello");

    it("should contain the something placeholder", function () {
        expect(result.something).toBe("hello");
    });
});