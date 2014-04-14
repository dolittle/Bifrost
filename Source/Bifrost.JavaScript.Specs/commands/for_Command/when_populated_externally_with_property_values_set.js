describe("when populated externally with property values set", function () {
    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            applyRulesTo: function (command) {
            },
            validateSilently: sinon.stub(),
            clearValidationMessagesFor: sinon.stub()
        },
        commandSecurityService: {
            getContextFor: function () {
                return {
                    continueWith: function () { }
                };
            }
        },
        options: {
        },
        region: {
            commands: []
        }
    }

    var commandType = Bifrost.commands.Command.extend(function () {
        this.someValue = ko.observable(42);
        this.someArray = ko.observableArray();
    });

    var newValues = {
        someValue: 43,
        someArray: [1, 2, 3]
    };

    var command = commandType.create(parameters);
    command.populatedExternally();
    command.populateFromExternalSource(newValues);

    it("should be considered ready", function () {
        expect(command.isReady()).toBe(true);
    });

    it("should not have changes", function () {
        expect(command.hasChanges()).toBe(false);
    });

    it("should not be considered ready to execute", function () {
        expect(command.isReadyToExecute()).toBe(false);
    });

    it("should clear any validation messages", function () {
        expect(parameters.commandValidationService.clearValidationMessagesFor.calledWith(command)).toBe(true);
    });
});