describe("when the saga command executor is busy", function () {
    var options,
        commandCoordinatorSpy,
        sce;

    beforeEach(function () {
        options = {
            commands:[Bifrost.commands.Command.create({})]
        };

        commandCoordinatorSpy = sinon.stub(Bifrost.commands.commandCoordinator, "handleForSagaCommandExecutor");

        sce = Bifrost.sagas.SagaCommandExecutor.create(options);
        sce.isBusy(true);
        sce.execute();
    });

    afterEach(function() {
        commandCoordinatorSpy.restore();
    });

    it("should not execute the saga command executor", function () {
        expect(commandCoordinatorSpy.called).toBeFalsy();
    });
});