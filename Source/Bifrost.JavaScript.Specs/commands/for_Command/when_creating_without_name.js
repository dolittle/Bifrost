describe("when creating without name", function () {
    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            applyRulesTo: sinon.stub(),
            validateSilently: sinon.stub()
        },
        commandSecurityService: {
            getContextFor: function () {
                return {
                    continueWith: function () { }
                };
            }
        },
        options: {
        },
        region: {
            commands: []
        },
        mapper: {}
    }
    var command = Bifrost.commands.Command.create(parameters);

    it("should not get validation rules", function () {
        expect(parameters.commandValidationService.applyRulesTo.called).toBe(false);
    });
});