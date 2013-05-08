describe("when executing without success", function () {
    var errorCommandResultReceived = null;
    var completeCommandResultReceived = null;
    var continueWithCallback = null;
    var handlePromise = {
        continueWith: function (callback) {
            continueWithCallback = callback;
        }
    };

    var parameters = {
        options: {
            failed: function (commandResult) {
                failedCommandResultReceived = commandResult;
            },
            succeeded: sinon.stub(),
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
            }
        },
        commandSecurityService: {
            getContextFor: function () {
                return {
                    continueWith: function () { }
                };
            }
        }
    }
    var command = Bifrost.commands.Command.create(parameters);
    command.execute();

    var commandResult = {
        success: false
    };

    continueWithCallback(commandResult);

    it("should call failed", function () {
        expect(failedCommandResultReceived).toBe(commandResult);
    });

    it("should not call success", function () {
        expect(parameters.options.succeeded.called).toBe(false);
    });

    it("should call complete", function () {
        expect(completeCommandResultReceived).toBe(commandResult);
    });
});