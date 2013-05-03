describe("when security context returns true for can execute", function () {
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
                            isAuthorized: ko.observable(true)
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

    it("should result in the command returning true for can execute", function () {
        expect(command.canExecute()).toBe(true);
    });
});