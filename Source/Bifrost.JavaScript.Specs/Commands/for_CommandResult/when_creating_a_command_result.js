describe("when an error occurs while executing a command", function () {
    var jsObject = {
            newProperty: "something"
        },
        commandResult;
    beforeEach(function () {
        commandResult = Bifrost.commands.CommandResult.createFrom(jsObject);
    });

    it("should extend with existsing js object", function () {
        expect(commandResult.newProperty).toBeDefined();
    });
});