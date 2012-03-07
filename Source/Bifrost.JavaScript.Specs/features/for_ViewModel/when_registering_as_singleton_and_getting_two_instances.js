describe("when registering as singleton and getting two instances", function () {
    var actualViewModel = function () { };

    var viewModel = Bifrost.features.ViewModel.create(actualViewModel, { isSingleton: true });
    var firstInstance = viewModel.getInstance();
    var secondInstance = viewModel.getInstance();

    it("should return same instance twice", function () {
        expect(firstInstance).toBe(secondInstance);
    });
});