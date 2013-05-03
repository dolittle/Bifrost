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
            error: sinon.stub(),
            success: function (commandResult) {
                successCommandResultReceived = commandResult;
            },
            complete: function (commandResult) {
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
        expect(parameters.options.error.called).toBe(false);
    });

    it("should call success", function () {
        expect(successCommandResultReceived).toBe(commandResult);
    });

    it("should call complete", function () {
        expect(completeCommandResultReceived).toBe(commandResult);
    });
});