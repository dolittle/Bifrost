describe("when executing", function () {
    var commandHandled = null;
    var commandResultReceived = null;
    var continueWithCallback = null;
    var handlePromise = {
        continueWith: function (callback) {
            continueWithCallback = callback;
        }
    };

    var parameters = {
        options: {
            beforeExecute: sinon.stub(),
            complete: function (commandResult) {
                commandResultReceived = commandResult;
            }
        },
        commandCoordinator: {
            handle: function (command) {
                commandHandled = command;
                return handlePromise;
            }
        }
    }
    var command = Bifrost.commands.Command.create(parameters);
    command.execute();
    continueWithCallback("Result");

    it("should call before execute", function () {
        expect(parameters.options.beforeExecute.called).toBe(true);
    });

    it("should delegate to the command coordinator", function () {
        expect(commandHandled).toBe(command);
    });

    it("should call complete", function () {
        expect(commandResultReceived).toBe("Result");
    });
});