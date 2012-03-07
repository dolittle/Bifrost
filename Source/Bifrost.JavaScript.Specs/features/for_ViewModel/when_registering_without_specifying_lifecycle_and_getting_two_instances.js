describe("when registering without specifying lifecycle and getting two instances", function () {
    var actualViewModel = function () { };

    var viewModel = Bifrost.features.ViewModel.create(actualViewModel);
    var firstInstance = viewModel.getInstance();
    var secondInstance = viewModel.getInstance();

    it("should return two different instances", function () {
        expect(firstInstance).not.toBe(secondInstance);
    });
});