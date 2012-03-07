function require() {
}

describe("when defining a view model", function() {
	var viewModelCreated = false;
	var viewModelType;
	var stateReceived;
	var singletonReceived;
	Bifrost.features.ViewModel = {
		create: function(viewModel, isSingleton, state) {
			viewModelCreated = true;
			viewModelType = viewModel;
			singletonReceived = isSingleton;
			stateReceived = state;
		}
	}
	
	
	function ViewModel() {}
	
	var stateObject = { something : "else" };
	
	var feature = Bifrost.features.Feature.create("something","something", false);
	feature.defineViewModel(ViewModel, true, stateObject);
	
	it("should create a view model", function() {
		expect(viewModelCreated).toBe(true);
	});
	
	it("should forward ViewModel as type", function() {
		expect(viewModelType).toBe(ViewModel);
	});
	
	it("should forward state", function() {
		expect(stateReceived).toBe(stateObject);
	});
	
	it("should forward singleton flag", function() {
		expect(singletonReceived).toBe(true);
	});
});