describe("when populated externally", function () {

    var securityContext = "SecurityContext";
    var command = Bifrost.commands.Command.create({
        commandCoordinator: {},
        commandValidationService: {
            applyRulesTo: function (command) {
                commandAppliedTo = command
            }
        },
        commandSecurityService: {
            getContextFor: function (command) {
                commandAskedForSecurityContext = command;
                return {
                    continueWith: function (callback) {
                        callback(securityContext);
                    }
                }
            }
        },
        options: {}
    });

    command.populatedExternally();

    it("should set the flag indicating it is populated externally to true", function () {
        expect(command.isPopulatedExternally()).toBe(true);
    });
});