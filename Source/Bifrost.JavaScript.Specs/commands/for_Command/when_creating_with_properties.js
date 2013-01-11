describe("when creating with properties", function () {
    var commandAppliedTo = null;
    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            applyRulesToProperties: function (command) {
                commandAppliedTo = command
            }
        },
        options: {
            properties: {
                integer: 5,
                number: 5.3,
                string: "hello"
            }
        }
    }
    var command = Bifrost.commands.Command.create(parameters);

    it("should add the integer property as an observable", function () {
        expect(ko.isObservable(command.integer)).toBe(true);
    });


    it("should apply validation rules to properties", function () {
        expect(commandAppliedTo).toBe(command);
    });
});