describe("when creating from an extended command with properties", function () {
    var commandAppliedTo = null;


    beforeEach(function () {
        var parameters = {
            commandCoordinator: {
            },
            commandValidationService: {
                applyRulesToProperties: function (command) {
                    commandAppliedTo = command
                }
            },
            options: {
                properties: {
                    integer: 5,
                    number: 5.3,
                    string: "hello"
                }
            }
        }


        Bifrost.commands.commandValidationService = Bifrost.commands.commandValidationService || {
            applyRulesToProperties: function () { }
        }
        sinon.stub(Bifrost.commands.commandValidationService);
        var command = Bifrost.commands.Command.create(parameters);
    });

    afterEach(function () {
        Bifrost.commands.commandValidationService.applyRulesToProperties.restore();
    });

    it("should add the integer property as an observable", function () {
        expect(ko.isObservable(command.integer)).toBe(true);
    });

    it("should apply validation rules to properties", function () {
        expect(commandAppliedTo).toBe(command);
    });
});