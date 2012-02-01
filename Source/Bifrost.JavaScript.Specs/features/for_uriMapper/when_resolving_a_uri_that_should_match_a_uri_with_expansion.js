Bifrost.features.UriMapping = (function () {
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

    Bifrost.features.uriMapper.clear();
    Bifrost.features.uriMapper.add("Home", "/Features/Home");
    Bifrost.features.uriMapper.add("{feature}/{subFeature}", "/Features/{feature}/{subFeature}");
    Bifrost.features.uriMapper.add("Something", "/Features/Else");

    Bifrost.features.uriMapper.allMappings()[1].shouldMatch = true;
    Bifrost.features.uriMapper.allMappings()[1].expectedUri = "/Features/Layout/Top";

    var result = Bifrost.features.uriMapper.resolve(uri);

    it("should resolve to the correct url", function () {
        expect(result).toEqual(expectedResult);
    });

});