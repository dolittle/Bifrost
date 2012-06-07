describe("when defining without specifying lifecycle and getting two instances", function () {
	Bifrost.features.ViewModel = Bifrost.features.ViewModel || {
		baseFor: function() {}
	};

    var actualViewModel = function () { 
		this.onActivated = function() {
		}
	};

    var viewModel = Bifrost.features.ViewModelDefinition.define(actualViewModel);
    var firstInstance = viewModel.getInstance();
    var secondInstance = viewModel.getInstance();

    it("should return two different instances", function () {
        expect(firstInstance).not.toBe(secondInstance);
    });
});