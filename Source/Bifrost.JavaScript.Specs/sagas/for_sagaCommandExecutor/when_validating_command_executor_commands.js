describe("when one command in a sagaCommandExecutor fails to validate", function () {
    var sce,
        options,
        beforeExecuteSpy,
        validateSpy,
        coordinatorSpy;

    beforeEach(function () {
        options = {
            sagaId: "bleh",
            commands: [Bifrost.commands.Command.create({
                    beforeExecute: function () { }
                }), 
                Bifrost.commands.Command.create({
                    beforeExecute: function () { }
                }), 
                Bifrost.commands.Command.create({
                    beforeExecute: function () { }
                })
            ],
            beforeExecute: sinon.stub()
        };

        validateSpy = sinon.spy(options.commands[0], "validate");
        beforeExecuteSpy = sinon.spy(options.commands[0].options, "beforeExecute");
        coordinatorSpy = sinon.stub(Bifrost.commands.commandCoordinator, "handleForSagaCommandExecutor");

        sce = Bifrost.sagas.SagaCommandExecutor.create(options);

        sce.execute();

    });

    afterEach(function () {
        coordinatorSpy.restore();
    });

    it("should call beforeExecute on every command", function () {
        expect(beforeExecuteSpy.called).toBeTruthy();
    });

    it("should call validate on every command", function () {
        expect(validateSpy.called).toBeTruthy();
    });

    it("should call beforeExecute from the options", function () {
        expect(options.beforeExecute.called).toBeTruthy();
    });

    it("should call handleSagaCommandExecutor on the command coordinator", function () {
        expect(coordinatorSpy.called).toBeTruthy();
    });


});