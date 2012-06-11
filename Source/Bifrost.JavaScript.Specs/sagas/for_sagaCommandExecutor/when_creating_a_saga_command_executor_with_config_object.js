describe("when creating a saga command executor with config object", function () {
    var sce,
        options;

    beforeEach(function () {
        options = {
            sagaId: "bleh",
            commands: [Bifrost.commands.Command.create({})],
            beforeExecute: function (){ },
            success: function () { },
            complete: function () { },
            error: function () { }
            
        };
        sce = Bifrost.sagas.SagaCommandExecutor.create(options);

    });

    it("should expose an execute method", function () {
        expect(sce.execute).toBeDefined();
    });

    it("should expose if it is busy", function () {
        expect(sce.isBusy).toBeDefined();
    });

    it("should use the sagaId from the options", function () {
        expect(sce.sagaId).toBe(options.sagaId);
    });

    it("should use the commands from the options", function () {
        expect(sce.commands).toBe(options.commands);
    });

    it("should use the success callback from the options", function () {
        expect(sce.options.success).toBe(options.success);
    });

    it("should use the beforeExecute callback from the options", function () {
        expect(sce.options.beforeExecute).toBe(options.beforeExecute);
    });

    it("should use the error callback from the options", function () {
        expect(sce.options.error).toBe(options.error);
    });

    it("should use the complete callback from the options", function () {
        expect(sce.options.complete).toBe(options.complete);
    });
});