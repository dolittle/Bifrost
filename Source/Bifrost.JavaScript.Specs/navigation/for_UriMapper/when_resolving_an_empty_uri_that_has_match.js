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

describe("when resolving an empty uri that has match", function () {
    var uriMapper = Bifrost.navigation.UriMapper.create();
    uriMapper.addMapping("{feature}", "/Features/{feature}");

    uriMapper.mappings[0].shouldMatch = true;
    uriMapper.mappings[0].expectedUri = "/Features//";

    var mappedUri = uriMapper.resolve("");

    it("should resolve to empty", function () {
        expect(mappedUri).toEqual("");
    });
});
