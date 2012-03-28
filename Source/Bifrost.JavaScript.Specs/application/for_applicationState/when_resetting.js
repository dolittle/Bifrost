describe("when resetting", function() {
	var feature = {};
	Bifrost.application.applicationState.activateFeature(feature);
	Bifrost.application.applicationState.reset();	
	
	it("should clear active features", function() {
		expect(Bifrost.application.applicationState.activeFeatures.length).toBe(0);
	});
});