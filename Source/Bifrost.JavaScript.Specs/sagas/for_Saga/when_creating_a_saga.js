describe("when creating a saga", function () {
    var saga,
        options;

    beforeEach(function () {
        options = {
        };
        saga = Bifrost.sagas.Saga.create(options);

    });

    it("should expose a way to create a command executor", function () {
        expect(saga.createCommandExecutor).toBeDefined();
    });

    it("should not expose a way to execute commands", function () {
        expect(saga.executeCommands).not.toBeDefined();
    });
});