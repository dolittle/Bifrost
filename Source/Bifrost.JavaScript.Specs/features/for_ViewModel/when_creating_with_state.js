describe("when creating with state", function () {
    var createCalled = false;

    Bifrost.features.FeatureState = {
        create: function() {
            createCalled = true;
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

    it("should create an instance of FeatureState", function () {
        expect(createCalled).toBe(true);    
    });
});