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
        command.resetAllValidationMessages();
    });

    it("should have a a resetAllValidationMessagess function", function () {
        expect(command.reseltAllValidationMessages).toBeDefined();
    });

    it("should not have any validation messages", function () {
        expect(options.parameters.computed.validator.messages).toBe("");
    });

    it("should not have any nested validation messages", function () {
        expect(options.parameters.plainObject.observable.validator.messages).toBe("");
    });
});