describe("when matching a string with wildcard in the middle", function () {
    var mapping = Bifrost.utils.StringMapping.create({
        format: "{something}/**/for_{else}",
        mappedFormat: "{something}.**.for_{else}"
    });
    var result = mapping.matches("this/is/a/wildcard/uri/for_things");

    it("should match", function () {
        expect(result).toBe(true);
    });
});