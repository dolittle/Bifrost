﻿describe("when populated externally without property values set", function () {

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
        options: {}
    });

    command.populatedExternally();

    it("should not be considered ready", function () {
        expect(command.isReady()).toBe(false);
    });
});