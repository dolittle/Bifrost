describe("when creating with parameters", function () {
    var format = "Something";
    var mappedFormat = "SomethingElse";

    var mapping = Bifrost.utils.StringMapping.create({
        format: format,
        mappedFormat: mappedFormat
    });

    it("should set format", function () {
        expect(mapping.format).toBe(format);
    });
    
    it("should set mapped format", function () {
        expect(mapping.mappedFormat).toBe(mappedFormat);
    });
});