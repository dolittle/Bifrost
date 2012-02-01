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

describe("when resolving a uri without any expansion", function () {
    var expectedResult = "/Features/About";
    var uri = "About-Us";

    Bifrost.features.uriMapper.clear();
    Bifrost.features.uriMapper.add("Home", "/Features/Home");
    Bifrost.features.uriMapper.add(uri, expectedResult);
    Bifrost.features.uriMapper.add("Something", "/Features/Else");

    var mappedUri = Bifrost.features.uriMapper.resolve(uri);

    it("should resolve to the correct url", function () {
        expect(mappedUri).toEqual("");
    });

});