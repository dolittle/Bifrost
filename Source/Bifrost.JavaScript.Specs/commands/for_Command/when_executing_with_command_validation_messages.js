describe("when executing with command validation messages", function () {
    var commandCoordinatorHandleCalled = false;
    var validationResults = [];
    var commandValidationMessages = [{},{}];
    var commandAppliedTo = null;
    var validationResultsApplied = null;
    var parameters = {
        options: {
            failed: sinon.stub(),
            succeeded: sinon.stub(),
            completed: sinon.stub()
        },
        commandCoordinator: {
            handle: function () {
                commandCoordinatorHandleCalled = true;
                return {
                    continueWith: function (callback) {
                        callback({
                            success: false,
                            invalid: true,
                            validationResults: validationResults,
                            commandValidationMessages: commandValidationMessages
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
            },
            validateSilently: sinon.stub()
        },
        commandSecurityService: {
            getContextFor: function () {
                return {
                    continueWith: function () { }
                };
            }
        },
        region: {
            commands: []
        },
        typeConverters: {}
    }

    var commandResult = null;
    beforeEach(function () {
        commandResult = Bifrost.commands.CommandResult;
        Bifrost.commands.CommandResult = {
            create: sinon.stub()
        };
    });

    afterEach(function () {
        Bifrost.commands.CommandResult = commandResult;
    });

    var command = Bifrost.commands.Command.create(parameters);
    command.execute();

    it("should call error", function () {
        expect(parameters.options.failed.called).toBe(true);
    });

    it("should not be able to execute", function () {
        expect(command.canExecute()).toBe(false);
    });

    it("should not be valid", function () {
        expect(command.isValid()).toBe(false);
    });

    it("should call the command coordinator", function () {
        expect(commandCoordinatorHandleCalled).toBe(true);
    });

    it("should not call success", function () {
        expect(parameters.options.succeeded.called).toBe(false);
    });

    it("should call complete", function () {
        expect(parameters.options.completed.called).toBe(true);
    });
});