describe("when deactivating one of three features", function() {
	var firstFeature = { first:"feature"};
	var secondFeature = { second:"feature"};
	var thirdFeature = { third:"feature"};
	
	beforeEach(function() {
		Bifrost.application.applicationState.activateFeature(firstFeature);
		Bifrost.application.applicationState.activateFeature(secondFeature);
		Bifrost.application.applicationState.activateFeature(thirdFeature);

		Bifrost.application.applicationState.deactivateFeature(secondFeature);
	});
	
	it("should have only two features", function() {
		expect(Bifrost.application.applicationState.activeFeatures.length).toBe(2);		
	});

	it("should remove feature", function() {
		expect(Bifrost.application.applicationState.activeFeatures.indexOf(secondFeature)).toBe(-1);

	});
	
	afterEach(function() {
		Bifrost.application.applicationState.reset();	
	});
});