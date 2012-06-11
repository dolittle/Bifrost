describe("when creating a saga command executor with commands array", function () {
    var sce,
        commands,
        sagaId = 123;

    beforeEach(function () {
        commands = [Bifrost.commands.Command.create({})];
        var saga = Bifrost.sagas.Saga.create({ Id: sagaId });
        sce = saga.createCommandExecutor(commands);

    });

    it("should use the commands provided", function () {
        expect(sce.commands).toBe(commands);
    });

    it("should use the saga id from the saga", function () {
        expect(sce.sagaId).toBe(sagaId);
    });
});