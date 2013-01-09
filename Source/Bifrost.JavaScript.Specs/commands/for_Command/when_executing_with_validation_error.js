describe("when executing with validation error", function () {
    var parameters = {
        options: {
            error: sinon.stub(),
            success: sinon.stub(),
            complete: sinon.stub()
        },
        commandCoordinator: {
            handle: sinon.stub()
        },
        commandValidationService: {
            applyRulesToProperties: function () { },
            validate: function (command) {
                return {
                    valid: false
                };
            }
        }
    }

    Bifrost.commands.CommandResult = {
        create: function () {
            return {
            }
        }
    };
    var command = Bifrost.commands.Command.create(parameters);
    command.execute();


    it("should call error", function () {
        expect(parameters.options.error.called).toBe(true);
    });

    it("should not call the command coordinator", function () {
        expect(parameters.commandCoordinator.handle.called).toBe(false);
    });

    it("should not call success", function () {
        expect(parameters.options.success.called).toBe(false);
    });

    it("should call complete", function () {
        expect(parameters.options.complete.called).toBe(true);
    });
});