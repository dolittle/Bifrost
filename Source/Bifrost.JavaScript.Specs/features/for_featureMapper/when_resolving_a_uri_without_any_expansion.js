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

describe("when resolving a uri without any expansion", function () {
    var expectedResult = "/Features/About";
    var uri = "About-Us";

    Bifrost.features.featureMapper.clear();
    Bifrost.features.featureMapper.add("Home", "/Features/Home");
    Bifrost.features.featureMapper.add(uri, expectedResult);
    Bifrost.features.featureMapper.add("Something", "/Features/Else");

    var mappedUri = Bifrost.features.featureMapper.resolve(uri);

    it("should resolve to the correct url", function () {
        expect(mappedUri).toEqual("");
    });

});