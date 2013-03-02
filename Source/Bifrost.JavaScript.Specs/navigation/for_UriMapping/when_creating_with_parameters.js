describe("when creating with parameters", function () {
    var uri = "Something";
    var mappedUri = "SomethingElse";

    var uriMapping = Bifrost.navigation.UriMapping.create({
        uri: uri,
        mappedUri: mappedUri
    });

    it("should set uri", function () {
        expect(uriMapping.uri).toBe(uri);
    });
    
    it("should set mapped uri", function () {
        expect(uriMapping.mappedUri).toBe(mappedUri);
    });

});