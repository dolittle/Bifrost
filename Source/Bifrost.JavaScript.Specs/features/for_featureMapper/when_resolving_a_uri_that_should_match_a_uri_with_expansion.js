Bifrost.features.FeatureMapping = (function () {
    function FeatureMapping(uri, mappedUri) {
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
            var featureMapping = new FeatureMapping(uri, mappedUri);
            return featureMapping;
        }
    }
})();

describe("when resolving a uri that should match a uri with expansion", function () {
    var expectedResult = "/Features/Layout/Top";
    var uri = "Layout/Top";

    Bifrost.features.featureMapper.clear();
    Bifrost.features.featureMapper.add("Home", "/Features/Home");
    Bifrost.features.featureMapper.add("{feature}/{subFeature}", "/Features/{feature}/{subFeature}");
    Bifrost.features.featureMapper.add("Something", "/Features/Else");

    Bifrost.features.featureMapper.allMappings()[1].shouldMatch = true;
    Bifrost.features.featureMapper.allMappings()[1].expectedUri = "/Features/Layout/Top";

    var result = Bifrost.features.featureMapper.resolve(uri);

    it("should resolve to the correct url", function () {
        expect(result).toEqual(expectedResult);
    });
});