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
            error: function (commandResult) {
                errorCommandResultReceived = commandResult;
            },
            success: sinon.stub(),
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
        success: false
    };

    continueWithCallback(commandResult);

    it("should call error", function () {
        expect(errorCommandResultReceived).toBe(commandResult);
    });

    it("should not call success", function () {
        expect(parameters.options.success.called).toBe(false);
    });

    it("should call complete", function () {
        expect(completeCommandResultReceived).toBe(commandResult);
    });
});