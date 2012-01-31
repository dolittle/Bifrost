Bifrost.features.UriMapping = (function() {
    function UriMapping(uri, mappedUri) {
        this.uri = uri;
        this.mappedUri = mappedUri;
    }

    return {
        create : function (uri, mappedUri) {
            return new UriMapping(uri, mappedUri);
        }
    }
})();

describe("when adding mapping", function () {
    var expectedUri = "something";
    var expectedMappedUri = "else";
    Bifrost.features.uriMapper.add(expectedUri, expectedMappedUri);
    var result = Bifrost.features.uriMapper.allMappings();


    it("should have one route", function () {
        expect(result.length).toBe(1);
    });

    it("should have one route with all parameters passed into it", function () {
        expect(result[0].uri).toEqual(expectedUri);
        expect(result[0].mappedUri).toEqual(expectedMappedUri);
    });
});