Bifrost.navigation.UriMapping = (function () {
    function UriMapping(uri, mappedUri) {
        var self = this;
        this.uri = uri;
        this.mappedUri = mappedUri;
        this.shouldMatch = false;
        this.expectedUri = mappedUri;

        this.matches = function (uri) {
            return self.shouldMatch;
        }

        this.resolve = function (uri) {
            return self.expectedUri;
        }
    }

    return {
        create: function (uri, mappedUri) {
            var uriMapping = new UriMapping(uri, mappedUri);
            return uriMapping;
        }
    }
})();

describe("when resolving a uri that should match a uri with expansion", function () {
    var expectedResult = "/Features/Layout/Top";
    var uri = "Layout/Top";

    var uriMapper = Bifrost.navigation.UriMapper.create();
    uriMapper.addMapping("Home", "/Features/Home");
    uriMapper.addMapping("{feature}/{subFeature}", "/Features/{feature}/{subFeature}");
    uriMapper.addMapping("Something", "/Features/Else");

    uriMapper.mappings[1].shouldMatch = true;
    uriMapper.mappings[1].expectedUri = "/Features/Layout/Top";

    var result = uriMapper.resolve(uri);

    it("should resolve to the correct url", function () {
        expect(result).toEqual(expectedResult);
    });
});