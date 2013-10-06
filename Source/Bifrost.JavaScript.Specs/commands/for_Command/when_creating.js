describe("when creating", function () {
    var commandAskedForSecurityContext = null;

    var securityContext = "SecurityContext";

    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            extendPropertiesWithoutValidation: sinon.mock(command).once(),
            getPropertiesWithValidation: sinon.mock(command).once()
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
        options: {
            name:"something"
        }
    }
    var command = null;

    command = Bifrost.commands.Command.create(parameters);

    it("should extend properties without validation with validator", function () {
        expect(parameters.commandValidationService.extendPropertiesWithoutValidation.called).toBe(true);
    });

    it("should get all properties with validation", function () {
        expect(parameters.commandValidationService.getPropertiesWithValidation.called).toBe(true);
    });

    it("should get security context for command", function () {
        expect(commandAskedForSecurityContext).toBe(command);
    });

    it("should set the security context on the command", function () {
        expect(command.securityContext()).toBe(securityContext);
    });
});