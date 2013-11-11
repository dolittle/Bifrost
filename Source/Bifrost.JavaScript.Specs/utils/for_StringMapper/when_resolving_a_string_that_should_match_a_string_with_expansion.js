describe("when resolving a string that should match a string with expansion", function () {
    var expectedResult = "/Features/Layout/Top";
    var input = "Layout/Top";

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
        stringMappingFactory : stringMappingFactory
    });
    mapper.addMapping("Home", "/Features/Home");
    mapper.addMapping("{feature}/{subFeature}", "/Features/{feature}/{subFeature}");
    mapper.addMapping("Something", "/Features/Else");

    mapper.mappings[1].shouldMatch = true;
    mapper.mappings[1].expectedFormat = "/Features/Layout/Top";

    var result = mapper.resolve(input);

    it("should resolve to the correct format", function () {
        expect(result).toEqual(expectedResult);
    });
});