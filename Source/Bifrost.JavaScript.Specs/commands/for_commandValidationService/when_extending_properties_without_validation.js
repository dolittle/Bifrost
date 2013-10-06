describe("when extending properties without validation", function () {
    var command = {
        something: ko.observable(),
        deep: {
            property: ko.observable()
        },
        property_with_validator: ko.observable(),
        object_property: {}
    };
    beforeEach(function () {
        ko.extenders.validation = function (target, options) {
            target.validator = {};
            return target;
        };

        command.property_with_validator.validator = "Existing";

        var commandValidationService = Bifrost.commands.commandValidationService.create();
        commandValidationService.extendPropertiesWithoutValidation(command);
    });

    it("should extend the top level property with validation", function () {
        expect(command.something.validator).toBeDefined();
    });

    it("should extend the deep property with validation", function () {
        expect(command.deep.property.validator).toBeDefined();
    });

    it("should not replace validator on property that already has a validator", function () {
        expect(command.property_with_validator.validator).toBe("Existing");
    });

    it("should not extend regular object literal", function () {
        expect(command.object_property.validator).toBeUndefined();
    });
});