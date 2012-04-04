describe("when defining without specifying lifecycle and getting two instances", function () {
    var actualViewModel = function () { };

    var viewModel = Bifrost.features.ViewModelDefinition.define(actualViewModel);
    var firstInstance = viewModel.getInstance();
    var secondInstance = viewModel.getInstance();

    it("should return two different instances", function () {
        expect(firstInstance).not.toBe(secondInstance);
    });
});