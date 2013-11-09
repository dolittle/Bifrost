describe("when creating with security context set on descendant", function () {
    var commandAskedForSecurityContext = null;

    var securityContext = "SecurityContext";

    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            extendPropertiesWithoutValidation: sinon.mock(command).once(),
            getValidatorsFor: sinon.mock(command).once(),
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
        options: {
            name:"something"
        }
    }
    var command = null;

    var expectedSecurityContext = "Something Preset";

    var commandType = Bifrost.commands.Command.extend(function () {
        this.securityContext(expectedSecurityContext);
    });

    command = commandType.create(parameters);

    it("should not change the security context on the command", function () {
        expect(command.securityContext()).toBe(expectedSecurityContext);
    });
});