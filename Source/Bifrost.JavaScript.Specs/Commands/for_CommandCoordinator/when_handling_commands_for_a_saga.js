describe("when handling commands for a saga", function () {
    var descriptorSpy,
        commandCoordinator,
        ajaxSpy,
        commands,
        options,
        saga;

    beforeEach(function () {


        descriptorSpy = sinon.spy(Bifrost.commands.CommandDescriptor, "createFrom"),
        commandCoordinator = Bifrost.commands.commandCoordinator,
        ajaxSpy = sinon.spy(jQuery, "ajax"),
        commands = [{ name: "1", onBeforeExecute: function () { } },
                    { name: "2", onBeforeExecute: function () { } }],
        options = { someOptions: {} },
        saga = { id: "" };

        (function becauseOf() {
            commandCoordinator.handleForSaga(saga, commands, options);
        })();
    });

    afterEach(function() {
        descriptorSpy.restore();
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

    afterEach(function () {
        ajaxSpy.restore();
    });

});