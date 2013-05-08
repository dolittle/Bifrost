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
            completed: function (commandResult) {
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
            applyRulesTo: function () { },
            validate: function (command) {
                validatedCommand = command;
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
    command = Bifrost.commands.Command.create(parameters);
    command.execute();
    continueWithCallback("Result");

    it("should be busy while executing", function () {
        expect(busyStatusOnExecute).toBe(true);
    });

    it("should not be busy after executing", function () {
        expect(busyStatusAfterExecute).toBe(false);
    });

    it("should call before execute", function () {
        expect(beforeExecuteCalled).toBe(true);
    });

    it("should delegate to the command coordinator", function () {
        expect(commandHandled).toBe(command);
    });

    it("should call complete", function () {
        expect(commandResultReceived).toBe("Result");
    });

    it("should validate command", function () {
        expect(validatedCommand).toBe(command);
    });
});