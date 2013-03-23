describe("when resolving a string with wildcard at the end", function () {
    var expectedResult = "this.is.a.wildcard.uri";
    var mapping = Bifrost.utils.StringMapping.create({
        format: "{something}/**/",
        mappedFormat: "{something}.**."
    });
    var result = mapping.resolve("this/is/a/wildcard/uri");

    it("should expand input uri to the mapped uri", function () {
        expect(result).toBe(expectedResult);
    });
});