Bifrost.StringMapping = (function () {
    function StringMapping(format, mappedFormat) {
        var self = this;
        this.format = format;
        this.mappedFormat = mappedFormat;
        this.shouldMatch = false;
        this.expectedFormat = mappedFormat;

        this.matches = function (format) {
            return self.shouldMatch;
        }

        this.resolve = function (format) {
            return self.expectedFormat;
        }
    }

    return {
        create: function (options) {
            var mapping = new StringMapping(options.format, options.mappedFormat);
            return mapping;
        }
    }
})();

describe("when resolving an empty string", function () {
    var mapper = Bifrost.StringMapper.create();
    mapper.addMapping("{feature}", "/Features/{feature}");

    mapper.mappings[0].shouldMatch = true;
    mapper.mappings[0].expectedFormat = "/Features//";

    var mappedFormat = mapper.resolve("");

    it("should resolve to empty", function () {
        expect(mappedFormat).toEqual("");
    });
});
