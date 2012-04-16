function require() {
}

describe("when defining a view model", function () {
    var viewModelDefined = false;
    var viewModelType;
    var optionsReceived;
    Bifrost.features.ViewModelDefinition = {
        define: function (viewModel, options) {
            viewModelDefined = true;
            viewModelType = viewModel;
            optionsReceived = options;
        }
    }


    function ViewModel() { }

    var options = { singleton: true, state: { something: "else"} }

    var feature = Bifrost.features.Feature.create("something", "something", false);
    feature.defineViewModel(ViewModel, options);

    it("should create a view model", function () {
        expect(viewModelDefined).toBe(true);
    });

    it("should forward ViewModel as type", function () {
        expect(viewModelType).toBe(ViewModel);
    });

    it("should forward options", function () {
        expect(optionsReceived).toBe(options);
    });
});