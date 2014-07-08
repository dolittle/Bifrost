describe("when populated externally without property values set", function () {

    var securityContext = "SecurityContext";
    var command = Bifrost.commands.Command.create({
        commandCoordinator: {},
        commandValidationService: {
            applyRulesTo: function (command) {
                commandAppliedTo = command
            },
            validateSilently: sinon.stub()
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
        options: {},
        region: {
            commands: []
        },
        typeConverters: {}
    });

    command.populatedExternally();

    it("should not be considered ready", function () {
        expect(command.isReady()).toBe(false);
    });

    it("should not be considered ready to execute", function () {
        expect(command.isReadyToExecute()).toBe(false);
    });
});