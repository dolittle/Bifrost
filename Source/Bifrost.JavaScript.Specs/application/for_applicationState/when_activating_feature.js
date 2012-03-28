describe("when activating feature", function() {
	var self = this;
	var feature = { something : 42 };
	
	beforeEach(function() {
		Bifrost.application.applicationState.activateFeature(feature);
	});
	
	it("should add feature to the active list", function() {
		expect(Bifrost.application.applicationState.activeFeatures[0]).toBe(feature);
	});
	
	
	afterEach(function() {
		Bifrost.application.applicationState.reset();
	});
});