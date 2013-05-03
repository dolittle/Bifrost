describe("when security context is null", function () {
    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            applyRulesTo: function (command) {
            }
        },
        commandSecurityService: {
            getContextFor: function (command) {
                return {
                    continueWith: function (callback) {
                    }
                }
            }
        },
        options: {
            name: "something"
        }
    }

    var command = Bifrost.commands.Command.create(parameters);

    it("should return true for can execute", function () {
        expect(command.canExecute()).toBe(false);
    });
});