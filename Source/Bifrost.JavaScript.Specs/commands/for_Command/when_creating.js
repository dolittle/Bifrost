describe("when creating", function () {
    var commandAppliedTo = null;
    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            applyRulesTo: function (command) {
                commandAppliedTo = command
            }
        }
    }
    var command = Bifrost.commands.Command.create(parameters);

    it("should apply validation rules to properties", function () {
        expect(commandAppliedTo).toBe(command);
    });
});