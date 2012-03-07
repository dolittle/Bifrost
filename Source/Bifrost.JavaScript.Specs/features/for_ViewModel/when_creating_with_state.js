describe("when creating without state", function() {
	function ViewModel() {
	}

	var state = {
		something : "Hello"
	};
	
	var viewModelDefinition = Bifrost.features.ViewModel.create(ViewModel, true, state);
	
	it("should have the state given to it", function() {
		expect(viewModelDefinition.state.something).toBe(state.something);
	});
});