
describe("when resolving a string with wildcard in the middle", function () {
    var expectedResult = "this.is.a.wildcard.uri.for_things";
    var mapping = Bifrost.utils.StringMapping.create({
        format: "{something}/**/for_{else}",
        mappedFormat: "{something}.**.for_{else}"
    });
    var result = mapping.resolve("this/is/a/wildcard/uri/for_things");

    it("should expand input uri to the mapped uri", function () {
        expect(result).toBe(expectedResult);
    });
});