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
            applyRulesTo: function () { },
            validate: function (command) {
                validatedCommand = command;
                return { valid: true };
            }
        }
    }

    var descendant = Bifrost.commands.Command.extend(function () {
        this.name = "descendant";
    });
    command = descendant.create(parameters);
    command.execute();
    continueWithCallback("Result");

    it("should pass along the descendant to the command coordinator", function () {
        expect(commandHandled).toBe(command);
    });
});