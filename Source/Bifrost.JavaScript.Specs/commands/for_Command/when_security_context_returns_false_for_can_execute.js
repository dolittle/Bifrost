describe("when security context returns false for can execute", function () {
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
                        callback({
                            canExecute: ko.observable(false)
                        });
                    }
                }
            }
        },
        options: {
            name: "something"
        }
    }

    var command = Bifrost.commands.Command.create(parameters);

    it("should result in the command returning false for can execute", function () {
        expect(command.canExecute()).toBe(false);
    });
});