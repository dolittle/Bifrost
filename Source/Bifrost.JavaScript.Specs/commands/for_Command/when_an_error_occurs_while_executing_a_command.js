describe("when an error occurs while executing a command", function () {
    var command, options, errorSpy;
    beforeEach(function () {
        errorSpy = sinon.spy();
        options = {
            error: errorSpy,
            success: function () {
            }
        };
        command = Bifrost.commands.Command.create(options);

        (function becauseOf() {
            command.onError();
        })();
    });

    it("should set the error state to true", function () {
        expect(command.hasError).toBeTruthy();
    });

    it("should call the supplied error function", function () {
        expect(errorSpy.calledOnce).toBeTruthy();
    });
});