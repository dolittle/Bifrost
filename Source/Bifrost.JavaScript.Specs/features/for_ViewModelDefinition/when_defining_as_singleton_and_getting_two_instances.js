describe("when defining as singleton and getting two instances", function () {
	Bifrost.features.ViewModel = Bifrost.features.ViewModel || {
		baseFor: function() {}
	};

    var actualViewModel = function () { };

    var viewModel = Bifrost.features.ViewModelDefinition.define(actualViewModel, { isSingleton: true });
    var firstInstance = viewModel.getInstance();
    var secondInstance = viewModel.getInstance();

    it("should return same instance twice", function () {
        expect(firstInstance).toBe(secondInstance);
    });
});