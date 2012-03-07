describe("when getting an instance from a transient that has state defined", function () {
    var expectedState = { hello: "world" };

    Bifrost.features.FeatureState = {
        create: function () {
            return expectedState;
        }
    };

    function ViewModel() {
    }

    var expectedState = {
        something: "Hello"
    };

    var viewModelDefinition = Bifrost.features.ViewModel.create(ViewModel, {
        singleton: false,
        state: expectedState
    });

    var viewModelInstance = viewModelDefinition.getInstance();


    it("should have an instance of state on the view model instance", function () {
        expect(viewModelInstance.state).toBe(expectedState);
    });
});