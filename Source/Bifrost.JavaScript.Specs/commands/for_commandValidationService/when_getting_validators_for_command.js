describe("when getting validators for command", function () {
    var command = {
        something: ko.observable(),
        deep: {
            property: ko.observable()
        },
        property_with_validator: ko.observable(),
        object_property: {}
    };
    var validators = [];
    beforeEach(function () {
        command.something.validator = "something";
        command.deep.property.validator = "deep.property";

        var commandValidationService = Bifrost.commands.commandValidationService.create();
        validators = commandValidationService.getValidatorsFor(command);
    });

    it("should return two validators", function () {
        expect(validators.length).toBe(2);
    });

    it("should have the something property validator", function () {
        expect(validators[0]).toBe("something");
    });

    it("should have the deep property validator", function () {
        expect(validators[1]).toBe("deep.property");
    });
});