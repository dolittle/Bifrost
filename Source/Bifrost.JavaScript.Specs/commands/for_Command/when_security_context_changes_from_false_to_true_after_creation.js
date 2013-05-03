describe("when security context changes from false to true", function () {
    var securityContext = {
        isAuthorized: ko.observable(false)
    };

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
                        callback(securityContext);
                    }
                }
            }
        },
        options: {
            name: "something"
        }
    }

    var command = Bifrost.commands.Command.create(parameters);
    securityContext.isAuthorized(true);

    it("should result in the command returning true for can execute", function () {
        expect(command.canExecute()).toBe(true);
    });
});