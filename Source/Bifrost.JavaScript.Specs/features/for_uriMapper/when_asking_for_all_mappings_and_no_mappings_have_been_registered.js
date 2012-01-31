describe("when asking for all mappings and no mappings have been registered", function () {
    var result = Bifrost.features.uriMapper.allMappings();
    it("should return an empty array", function () {
        expect(result.length).toBe(0);
    });
});

