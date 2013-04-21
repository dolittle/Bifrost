describe("when creating without name", function () {
    var parameters = {
        commandCoordinator: {
        },
        commandValidationService: {
            applyRulesTo: sinon.stub()
        },
        options: {
        }
    }
    var command = Bifrost.commands.Command.create(parameters);

    it("should not get validation rules", function () {
        expect(parameters.commandValidationService.applyRulesTo.called).toBe(false);
    });
});