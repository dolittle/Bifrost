describe("when registering as transient and getting two instances", function () {
    var actualViewModel = function () { };

    var viewModel = Bifrost.features.ViewModel.create(actualViewModel, { isSingleton : false });
    var firstInstance = viewModel.getInstance();
    var secondInstance = viewModel.getInstance();

    it("should return two different instances", function () {
        expect(firstInstance).toNotBe(secondInstance);
    });
});