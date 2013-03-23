describe("when adding mapping", function () {
    var uriPassed = null;
    var mappedUriPassed = null;
    var createCalled = false;

    var mapping = {};

    
    Bifrost.navigation.UriMapping = {
        create: function (options) {
            createCalled = true;
            uriPassed = options.uri;
            mappedUriPassed = options.mappedUri;

            return mapping;
        }
    };

    var uriMapper = Bifrost.navigation.UriMapper.create();
    uriMapper.addMapping("Something", "else");

    it("should create a new uri mapping", function () {
        expect(createCalled).toBe(true);
    });

    it("should pass the uri to the mapping", function () {
        expect(uriPassed).toBe("Something");
    });

    it("should pass the mapped uri to the mapping", function () {
        expect(mappedUriPassed).toBe("else");
    });

    it("should add the mapping to itself", function () {
        expect(uriMapper.mappings[0]).toBe(mapping);
    });
});