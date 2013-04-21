describe("when executing with server side validation error", function () {
    var commandCoordinatorHandleCalled = false;
    var validationResults = [{}, {}];
    var commandAppliedTo = null;
    var validationResultsApplied = null;
    var parameters = {
        options: {
            error: sinon.stub(),
            success: sinon.stub(),
            complete: sinon.stub()
        },
        commandCoordinator: {
            handle: function () {
                commandCoordinatorHandleCalled = true;
                return {
                    continueWith: function (callback) {
                        callback({
                            success: false,
                            invalid: true,
                            validationResults: validationResults
                        });
                    }
                }
            }
        },
        commandValidationService: {
            applyRulesTo: function () { },
            applyValidationResultToProperties: function (command, validationResults) {
                commandAppliedTo = command;
                validationResultsApplied = validationResults;
            },
            validate: function (command) {
                return {
                    valid: true
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

    it("should apply validation results to the command", function () {
        expect(commandAppliedTo).toBe(command);
    });

    it("should apply the validation results from the command result", function () {
        expect(validationResultsApplied).toBe(validationResults);
    });

    it("should call the command coordinator", function () {
        expect(commandCoordinatorHandleCalled).toBe(true);
    });

    it("should not call success", function () {
        expect(parameters.options.success.called).toBe(false);
    });

    it("should call complete", function () {
        expect(parameters.options.complete.called).toBe(true);
    });
});