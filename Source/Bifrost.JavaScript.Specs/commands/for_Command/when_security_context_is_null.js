describe("when security context is null", function () {
    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            extendPropertiesWithoutValidation: sinon.stub(),
            getValidatorsFor: sinon.stub(),
            validateSilently: sinon.stub()
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
        },
        region: {
            commands: []
        }
    }

    var command = Bifrost.commands.Command.create(parameters);

    it("should return true for can execute", function () {
        expect(command.canExecute()).toBe(false);
    });
});