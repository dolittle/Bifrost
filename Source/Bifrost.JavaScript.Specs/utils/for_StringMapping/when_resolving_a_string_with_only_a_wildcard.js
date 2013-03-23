describe("when resolving a string with only a wildcard", function () {
    var expectedResult = "this.is.a.wildcard.uri.for_things";
    var mapping = Bifrost.utils.StringMapping.create({
        format: "**/",
        mappedFormat: "**."
    });
    var result = mapping.resolve("this/is/a/wildcard/uri/for_things");

    it("should expand input uri to the mapped uri", function () {
        expect(result).toBe(expectedResult);
    });
});