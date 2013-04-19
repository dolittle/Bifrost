describe("when adding mapping", function () {
    var formatPassed = null;
    var mappedFormatPassed = null;
    var createCalled = false;

    var mapping = {};

    
    Bifrost.StringMapping = {
        create: function (options) {
            createCalled = true;
            formatPassed = options.format;
            mappedFormatPassed = options.mappedFormat;

            return mapping;
        }
    };

    var mapper = Bifrost.StringMapper.create();
    mapper.addMapping("Something", "else");

    it("should create a new uri mapping", function () {
        expect(createCalled).toBe(true);
    });

    it("should pass the format to the mapping", function () {
        expect(formatPassed).toBe("Something");
    });

    it("should pass the mapped format to the mapping", function () {
        expect(mappedFormatPassed).toBe("else");
    });

    it("should add the mapping to itself", function () {
        expect(mapper.mappings[0]).toBe(mapping);
    });
});