describe("when executing a command which can execute", function () {

    var onBeforeExecuteSpy,
        coordinatorSpy, 
        command;

    beforeEach(function () {
        var options = {
            error: function () {
                print("Error");
            },
            success: function () {
            }
        };
        command = Bifrost.commands.Command.create(options);
        Bifrost.namespace("Bifrost.commands.commandCoordinator");

        onBeforeExecuteSpy = sinon.spy(command, "onBeforeExecute");
        coordinatorSpy = sinon.spy(Bifrost.commands.commandCoordinator, 'handle');

        command.execute();
    });

    afterEach(function () {
        coordinatorSpy.restore();
    });

    it("should reset any errors before execution", function () {
        command.hasError = true;

        expect(command.hasError).toBeTruthy();
        command.execute();

        expect(command.hasError).toBeFalsy();
    });

    it("should call onBeforeExecute", function () {
        expect(onBeforeExecuteSpy.calledOnce).toBeTruthy();
    });

    it("should call the commandCoordinator", function () {
        expect(coordinatorSpy.calledOnce).toBeTruthy();
    });

    it("should set the command to the busy state", function () {
        expect(command.isBusy).toBeTruthy();
    });

});