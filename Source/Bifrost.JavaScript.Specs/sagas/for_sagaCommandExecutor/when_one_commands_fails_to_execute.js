describe("when one command fails to execute", function () {
    var sce,
        options,
        successfullyExecutedSpy;

    beforeEach(function () {
        options = {
            sagaId: "bleh",
            commands: [
                Bifrost.commands.Command.create({
                    beforeExecute: function () {
                    }
                }),
                Bifrost.commands.Command.create({
                    beforeExecute: function () {
                    }
                }),
                Bifrost.commands.Command.create({
                    beforeExecute: function () {
                    }
                })
            ],
            beforeExecute: sinon.stub(),
            success: sinon.stub(),
            complete: sinon.stub(),
            error: sinon.stub()
        };

        successfullyExecutedSpy = sinon.stub();
        for (var i = 0; i < options.commands.length; i++) {
            var command = options.commands[i];
            if (i == 1) {
                command.successfullyExecuted = function () { return false; };
            } else {
                command.successfullyExecuted = successfullyExecutedSpy;
            }
        }
        sce = Bifrost.sagas.SagaCommandExecutor.create(options);
        sce.onComplete();

    });

    it("should check if all commands have successfully executed", function () {
        expect(successfullyExecutedSpy.callCount).toBe(1);
    });

    it("should not call options.success", function () {
        expect(options.success.called).toBeFalsy();
    });

    it("should not call options.complete", function () {
        expect(options.complete.called).toBeFalsy();
    });

    it("should call options.error", function () {
        expect(options.error.called).toBeTruthy();
    });

    it("should not be busy", function () {
        expect(sce.isBusy()).toBeFalsy();
    });
});