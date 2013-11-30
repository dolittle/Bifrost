describe("when executing with success and valid", function () {
    var successCommandResultReceived = null;
    var completeCommandResultReceived = null;
    var continueWithCallback = null;
    var handlePromise = {
        continueWith: function (callback) {
            continueWithCallback = callback;
        }
    };

    var parameters = {
        options: {
            failed: sinon.stub(),
            succeeded: function (commandResult) {
                successCommandResultReceived = commandResult;
            },
            completed: function (commandResult) {
                completeCommandResultReceived = commandResult;
            }
        },
        commandCoordinator: {
            handle: function (command) {
                commandHandled = command;
                return handlePromise;
            }
        },
        commandValidationService: {
            applyRulesTo: function () { },
            validate: function (command) {
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
        }
    }
    var command = Bifrost.commands.Command.create(parameters);

    command.execute();

    var commandResult = {
        success: true,
        valid: true
    };

    continueWithCallback(commandResult);

    it("should not call error", function () {
        expect(parameters.options.failed.called).toBe(false);
    });

    it("should call success", function () {
        expect(successCommandResultReceived).toBe(commandResult);
    });

    it("should call complete", function () {
        expect(completeCommandResultReceived).toBe(commandResult);
    });
});