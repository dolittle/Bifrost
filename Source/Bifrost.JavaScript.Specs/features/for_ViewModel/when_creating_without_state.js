describe("when creating without state", function() {
	function ViewModel() {
	}
	
	var viewModelDefinition = Bifrost.features.ViewModel.create(ViewModel, true);
	
	it("should have state", function() {
		expect(viewModelDefinition.state).not.toBeUndefined();
	});
	
	it("should have an empty state", function() {
		var propertyCount = 0;
		for( var property in viewModelDefinition.state ) {
			if( viewModelDefinition.state.hasOwnProperty(property) ) {
				propertyCount++;
			}
		}
		expect(propertyCount).toBe(0);
	});
});