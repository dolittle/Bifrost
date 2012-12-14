describe("when an error occurs while executing a command", function () {
    var jsObject = {
		newProperty: "something"
	}
    beforeEach(function () {
        
    });

    it("should extend with existsing js object", function () {
		var commandResult = Bifrost.commands.CommandResult.createFrom(jsObject);
        expect(commandResult.newProperty).toBeDefined();
    });
});