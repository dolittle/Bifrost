describe("when resolving a string with wildcard in the middle", function () {
    var expectedResult = "this.is.a.wildcard.string.for_things";
    var mapping = Bifrost.StringMapping.create({
        format: "{something}/**/for_{else}",
        mappedFormat: "{something}.**.for_{else}"
    });
    var result = mapping.resolve("this/is/a/wildcard/string/for_things");

    it("should expand input string to the mapped string", function () {
        expect(result).toBe(expectedResult);
    });
});