describe("when asking for all mappings and no mappings have been registered", function () {
	var uriMapper = Bifrost.navigation.UriMapper.create();
    it("should return an empty array", function () {
        expect(uriMapper.mappings.length).toBe(0);
    });
});

