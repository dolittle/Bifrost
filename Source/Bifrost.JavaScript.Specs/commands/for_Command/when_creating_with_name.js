describe("when creating with name", function () {
    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            extendPropertiesWithoutValidation: sinon.stub(),
            getValidatorsFor: sinon.stub()
        },
        commandSecurityService: {
            getContextFor: function () {
                return {
                    continueWith: function () { }
                };
            }
        },
        options: {
            name: "something"
        }
    }
    var command = Bifrost.commands.Command.create(parameters);

    it("should set name on the command", function () {
        expect(command.name).toBe("something");
    });
});