describe("when resolving a string without any expansion", function () {
    var expectedResult = "/Features/About";
    var input = "About-Us";

    var stringMappingFactory = {
        create: function (format, mappedFormat) {
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
            return new StringMapping(format, mappedFormat);
        }
    }

    var mapper = Bifrost.StringMapper.create({
        stringMappingFactory: stringMappingFactory
    });
    mapper.addMapping("Home", "/Features/Home");
    mapper.addMapping(input, expectedResult);
    mapper.addMapping("Something", "/Features/Else");

    var mapped = mapper.resolve(input);

    it("should resolve to the correct url", function () {
        expect(mapped).toEqual("");
    });
});