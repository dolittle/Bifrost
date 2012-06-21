describe("when asking for all mappings and no mappings have been registered", function () {
	var mappings;
	
	beforeEach(function() {
		Bifrost.features.featureMapper.clear();
		mappings = Bifrost.features.featureMapper.allMappings();
	});
	
    it("should return an empty array", function () {
        expect(mappings.length).toBe(0);
    });
});

