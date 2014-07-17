describe("when executing", function () {
    var commandHandled = null;
    var validatedCommand = null;
    var commandResultReceived = null;
    var continueWithCallback = null;
    var busyStatusOnExecute = null;
    var busyStatusAfterExecute = null;
    var beforeExecuteCalled = false;

    var handlePromise = {
        continueWith: function (callback) {
            continueWithCallback = callback;
        }
    };

    var command = null;

    var parameters = {
        options: {
            beforeExecute: function () {
                busyStatusOnExecute = command.isBusy();
                beforeExecuteCalled = true;
            },
            complete: function (commandResult) {
                commandResultReceived = commandResult;
                busyStatusAfterExecute = command.isBusy();
            }
        },
        commandCoordinator: {
            handle: function (command) {
                commandHandled = command;
                return handlePromise;
            }
        },
        commandValidationService: {
            extendPropertiesWithoutValidation: sinon.stub(),
            getValidatorsFor: sinon.stub(),
            validate: function (command) {
                validatedCommand = command;
                return { valid: true };
            },
            validateSilently: sinon.stub()
        },
        commandSecurityService: {
            getContextFor: function () {
                return {
                    continueWith: function () { }
                };
            }
        },
        region: {
            commands: []
        },
        mapper: {}
    };

    var descendant = Bifrost.commands.Command.extend(function () {
        this.name = "descendant";
        this.firstProperty = ko.observable(42);
        this.secondProperty = ko.observable("43");

    });
    command = descendant.create(parameters);

    command.firstProperty(43);
    command.secondProperty("44");

    command.firstProperty.setInitialValue = sinon.mock().withArgs(43).once();
    command.secondProperty.setInitialValue = sinon.mock().withArgs("44").once();
    
    command.execute();
    continueWithCallback("Result");

    it("should pass along the descendant to the command coordinator", function () {
        expect(commandHandled).toBe(command);
    });

    it("should set initial value for the first property to the changed value", function () {
        expect(command.firstProperty.setInitialValue.verify()).toBe(true);
    });

    it("should set initial value for the second property to the changed value", function () {
        expect(command.firstProperty.setInitialValue.verify()).toBe(true);
    });

});