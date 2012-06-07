describe("when the command is busy", function () {
    var options,
        commandCoordinatorSpy,
        command;

    beforeEach(function () {
        options = {
            parameters: {
                computed: ko.computed(function () { return "test"; }),
                
            }
        };

        commandCoordinatorSpy = sinon.stub(Bifrost.commands.commandCoordinator, "handle");

        command = Bifrost.commands.Command.create(options);
        command.isBusy(true);
        command.execute();
    });

    afterEach(function() {
        commandCoordinatorSpy.restore();
    });

    it("should not execute the command", function () {
        expect(commandCoordinatorSpy.called).toBeFalsy();
    });
});