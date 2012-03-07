describe("when creating with state", function () {
    var state = {
        anInteger: 5,
        aString: "something"
    };

    var featureState = Bifrost.features.FeatureState.create(state);

    it("should return an instance", function () {
        expect(featureState).not.toBeUndefined();
    });

    it("should pass along default values to properties kept in state", function () {
        expect(featureState.anInteger()).toBe(state.anInteger);
        expect(featureState.aString()).toBe(state.aString);
    });
});