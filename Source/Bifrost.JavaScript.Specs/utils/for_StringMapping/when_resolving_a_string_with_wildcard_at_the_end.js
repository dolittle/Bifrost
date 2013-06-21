describe("when resolving a string with wildcard at the end", function () {
    var expectedResult = "this.is.a.wildcard.string";
    var mapping = Bifrost.StringMapping.create({
        format: "{something}/**/",
        mappedFormat: "{something}.**."
    });
    var result = mapping.resolve("this/is/a/wildcard/string");

    it("should expand input string to the mapped string", function () {
        expect(result).toBe(expectedResult);
    });
});