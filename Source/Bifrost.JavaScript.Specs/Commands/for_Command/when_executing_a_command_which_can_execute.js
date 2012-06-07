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

        onBeforeExecuteSpy = sinon.spy(command, "onBeforeExecute");
        coordinatorSpy = sinon.spy(Bifrost.commands.commandCoordinator, 'handle');

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
        command.execute();
        expect(onBeforeExecuteSpy.calledOnce).toBeTruthy();
    });

    it("should call the commandCoordinator", function () {
        command.execute();
        expect(coordinatorSpy.calledOnce).toBeTruthy();
    });

    it("should set the command to the busy state", function () {
        command.execute();
        expect(command.isBusy).toBeTruthy();
    });

});