function require() {
}

describe("when defining a view model", function () {
    var viewModelCreated = false;
    var viewModelType;
    var optionsReceived;
    Bifrost.features.ViewModel = {
        create: function (viewModel, options) {
            viewModelCreated = true;
            viewModelType = viewModel;
            optionsReceived = options;
        }
    }


    function ViewModel() { }

    var options = { singleton: true, state: { something: "else"} }

    var feature = Bifrost.features.Feature.create("something", "something", false);
    feature.defineViewModel(ViewModel, options);

    it("should create a view model", function () {
        expect(viewModelCreated).toBe(true);
    });

    it("should forward ViewModel as type", function () {
        expect(viewModelType).toBe(ViewModel);
    });

    it("should forward options", function () {
        expect(optionsReceived).toBe(options);
    });
});