describe("when creating a saga command executor with commands array", function () {
    var sce,
        commands,
        options,
        sagaId = 123,
        success = function () { },
        error = function () { },
        complete = function () { };

    beforeEach(function () {
        commands = [Bifrost.commands.Command.create({})];
        options = {
            commands: commands,
            success: success,
            error: error,
            complete: complete
        };
        var saga = Bifrost.sagas.Saga.create({ Id: sagaId });
        sce = saga.createCommandExecutor(options);

    });

    it("should use the commands provided", function () {
        expect(sce.commands).toBe(commands);
    });

    it("should use the saga id from the saga", function () {
        expect(sce.sagaId).toBe(sagaId);
    });
    it("should use the success callback", function () {
        expect(sce.options.success).toBe(success);
    });
    it("should use the error callback", function () {
        expect(sce.options.error).toBe(error);
    });
    it("should use the complete callback", function () {
        expect(sce.options.complete).toBe(complete);
    });
});