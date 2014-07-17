describe("when security context returns true for can execute", function () {
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
                        callback({
                            isAuthorized: ko.observable(true)
                        });
                    }
                }
            }
        },
        options: {
            name: "something"
        },
        region: {
            commands: []
        },
        mapper: {}
    }

    var command = Bifrost.commands.Command.create(parameters);

    it("should result in the command returning true for can execute", function () {
        expect(command.canExecute()).toBe(true);
    });
});