describe("when matching a string that should match", function () {
    var mapping = Bifrost.utils.StringMapping.create({
        format: "{something}/{else}",
        mappedFormat: "whatevva"
    });
    var result = mapping.matches("hello/there");

    it("should match", function () {
        expect(result).toBe(true);
    });
});