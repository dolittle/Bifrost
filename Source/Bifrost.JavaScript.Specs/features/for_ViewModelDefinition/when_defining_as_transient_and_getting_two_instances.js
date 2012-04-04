describe("when defining as transient and getting two instances", function () {
    var actualViewModel = function () { };

    var viewModel = Bifrost.features.ViewModelDefinition.define(actualViewModel, { isSingleton : false });
    var firstInstance = viewModel.getInstance();
    var secondInstance = viewModel.getInstance();

    it("should return two different instances", function () {
        expect(firstInstance).toNotBe(secondInstance);
    });
});