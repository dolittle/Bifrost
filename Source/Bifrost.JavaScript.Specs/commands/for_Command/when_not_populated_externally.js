describe("when not populated externally", function () {

    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            extendPropertiesWithoutValidation: sinon.stub(),
            getPropertiesWithValidation: sinon.stub()
        },
        commandSecurityService: {
            getContextFor: function (command) {
                commandAskedForSecurityContext = command;
                return {
                    continueWith: function (callback) {
                        callback("blah");
                    }
                }
            }
        },
        options: {
            name: "something"
        }
    }
    var command = Bifrost.commands.Command.create(parameters);

    it("should indicate that it is not populated externally", function() {
        expect(command.isPopulatedExternally()).toBe(false);
    });

    it("should be considered ready", function () {
        expect(command.isReady()).toBe(true);
    });

    it("should not have changes", function () {
        //expect(command.hasChanges()).toBe(false);
    });
});
