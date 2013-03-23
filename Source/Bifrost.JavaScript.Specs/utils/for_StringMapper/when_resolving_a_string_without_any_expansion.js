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
        create: function (format, mappedFormat) {
            var mapping = new StringMapping(format, mappedFormat);
            return mapping;
        }
    }
})();

describe("when resolving a string without any expansion", function () {
    var expectedResult = "/Features/About";
    var input = "About-Us";

    var mapper = Bifrost.StringMapper.create();
    mapper.addMapping("Home", "/Features/Home");
    mapper.addMapping(input, expectedResult);
    mapper.addMapping("Something", "/Features/Else");

    var mapped = mapper.resolve(input);

    it("should resolve to the correct url", function () {
        expect(mapped).toEqual("");
    });
});