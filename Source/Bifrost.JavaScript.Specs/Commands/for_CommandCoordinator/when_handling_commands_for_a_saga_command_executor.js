describe("when handling commands for a saga command executor", function () {
    var descriptorSpy,
        commandCoordinator,
        ajaxSpy,
        onCompleteSpy,
        commands,
        options,
        sce;

    beforeEach(function () {


        descriptorSpy = sinon.spy(Bifrost.commands.CommandDescriptor, "createFrom"),
        commandCoordinator = Bifrost.commands.commandCoordinator,
        ajaxSpy = sinon.spy(jQuery, "ajax"),
        commands = [{ name: "1", onBeforeExecute: function () { }, parameters: {} },
                    { name: "2", onBeforeExecute: function () { }, parameters: {}}],
        sce = { sagaId: "", onComplete: function () { } };
        onCompleteSpy = sinon.stub(sce, "onComplete");

        (function becauseOf() {
            commandCoordinator.handleForSagaCommandExecutor(sce, commands);
        })();
    });

    afterEach(function () {
        descriptorSpy.restore();
        ajaxSpy.restore();
    });

    it("should create methodParameters to be sent to the server", function () {
        expect(descriptorSpy.calledTwice).toBeTruthy();
        expect(descriptorSpy.calledWith(commands[0])).toBeTruthy();
        expect(descriptorSpy.calledWith(commands[1])).toBeTruthy();
    });

    it("should post the command to the server with the default handleUrl", function () {
        var handleUrl = ajaxSpy.getCall(0).args[0].url;
        expect(handleUrl).toEqual("/CommandCoordinator/HandleForSaga");
    });

    it("should notify the saga command executor that the command is executed", function () {
        ajaxSpy.firstCall.args[0].complete({responseText: "[]"});
        expect(onCompleteSpy.called).toBeTruthy();
    });

    afterEach(function () {
        ajaxSpy.restore();
    });

});