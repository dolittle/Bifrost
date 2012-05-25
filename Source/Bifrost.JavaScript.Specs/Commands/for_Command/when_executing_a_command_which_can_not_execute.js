describe("when executing a command which can not execute", function () {

    var onBeforeExecuteSpy, coordinatorSpy, command, canExecuteStub;

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
        Bifrost.commands.commandCoordinator.handle = function () { };

        onBeforeExecuteSpy = sinon.spy(command, "onBeforeExecute");
        coordinatorSpy = sinon.spy(Bifrost.commands.commandCoordinator, 'handle');

        canExecuteStub = sinon.stub(command, "canExecute");
        canExecuteStub.returns(false);

        command.execute();
    });

    afterEach(function () {
        coordinatorSpy.restore();
    })

    it("should reset any errors before execution", function () {
        command.hasError = true;

        expect(command.hasError).toBeTruthy();
        command.execute();

        expect(command.hasError).toBeFalsy();
    });

    it("should call onBeforeExecute", function () {
        expect(onBeforeExecuteSpy.calledOnce).toBeTruthy();
    });

    it("should not call the commandCoordinator", function () {
        expect(coordinatorSpy.calledOnce).toBeFalsy();
    });

    it("should not set the command to the busy state", function () {
        expect(command.isBusy()).toBeFalsy();
    });

});