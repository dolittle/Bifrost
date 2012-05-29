describe("when getting an instance", function () {
	Bifrost.features.ViewModel = Bifrost.features.ViewModel || {
		baseFor: function() {}
	};
	
	var onActivatedCalled = false;
    var actualViewModel = function () { 
		this.onActivated = function() {
			onActivatedCalled = true;
		}
	};

    var viewModel = Bifrost.features.ViewModelDefinition.define(actualViewModel, { isSingleton : false });
    var instance = viewModel.getInstance();

	it("should call onActivated on viewModel", function() {
		expect(onActivatedCalled).toBe(true);
	});
	
	
});
