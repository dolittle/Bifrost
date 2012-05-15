describe("when reseting validation messages in the command", function () {
    var options,
        command;

    beforeEach(function () {
        options = {
            error: function () {
                print("Error");
            },
            success: function () {
            },
            parameters: {
                plainValue: "test",
                computed: ko.computed(function () { return "test"; }),
                plainObject: {
                    plainValue: "test",
                    observable: ko.observable("test")
                }
            }
        };
        command = Bifrost.commands.Command.create(options);
        command.parameters.computed.validator.message("erro message which should be cleared");
        command.parameters.plainObject.observable.validator.message("erro message which should be cleared");
        command.validator.message("some message here");
        command.resetAllValidationMessages();
    });

    it("should have a a resetAllValidationMessagess function", function () {
        expect(command.resetAllValidationMessages).toBeDefined();
    });

    it("should not have any validation messages", function () {
        expect(options.parameters.computed.validator.message()).toBe("");
    });

    it("should not have any nested validation messages", function () {
        expect(options.parameters.plainObject.observable.validator.message()).toBe("");
    });

    it("should remove the command validation message", function () {
        expect(command.validator.message()).toBe("");
    });

    it("should be valid", function () {
        expect(command.parametersAreValid()).toBe(true);
    });
});