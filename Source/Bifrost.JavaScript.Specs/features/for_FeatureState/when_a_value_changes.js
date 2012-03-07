describe("when a value changes", function () {
    var expectedNewValue = 6;
    var state = {
        something: 5
    };
    var changeCalled = false;
    var propertyReceived;
    var newValueReceived;
    var featureState = Bifrost.features.FeatureState.create(state, {
        changed: function (property, newValue) {
            changeCalled = true;
            propertyReceived = property;
            newValueReceived = newValue;
        }
    });

    featureState.something(expectedNewValue);

    it("should call the changed callback", function () {
        expect(changeCalled).toBe(true);
    });

    it("should forward the property name to the changed callback", function () {
        expect(propertyReceived).toBe("something");
    });

    it("should forward the new value to the changed callback", function () {
        expect(newValueReceived).toBe(expectedNewValue);
    });
});