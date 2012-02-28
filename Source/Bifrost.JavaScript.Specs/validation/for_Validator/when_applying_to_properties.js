describe("when applying to properties", function () {
    it("should add a validator to all property instances", function () {
        var itemWithProperties = {
            first: {},
            second: {},
            third: {}
        };

        Bifrost.validation.Validator.applyToProperties(itemWithProperties, {});
        var validatorCount = 0;
        var propertyCount = 0;

        for (var property in itemWithProperties) {
            if (itemWithProperties.hasOwnProperty(property)) {
                propertyCount++;
                if (typeof itemWithProperties[property].validator !== "undefined") {
                    validatorCount++;
                }
            }
        }
        expect(validatorCount).toBe(propertyCount);
    });
});