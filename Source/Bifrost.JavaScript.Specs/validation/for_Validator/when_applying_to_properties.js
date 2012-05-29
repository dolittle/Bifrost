describe("when applying to properties", function () {

    var itemWithProperties,
        validatorCount,
        propertyCount;

    beforeEach(function() {
        itemWithProperties = {
            first: {},
            second: {},
            third: {}
        };

        Bifrost.validation.Validator.applyToProperties(itemWithProperties, {});
        validatorCount = 0;
        propertyCount = 0;

        for (var property in itemWithProperties) {
            if (itemWithProperties.hasOwnProperty(property)) {
                propertyCount++;
                if (typeof itemWithProperties[property].validator !== "undefined") {
                    validatorCount++;
                }
            }
        }
    });

    it("should add a validator to all property instances", function () {
        
        expect(validatorCount).toBe(propertyCount);
    });
});