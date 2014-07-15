describe("when creating a command result", function () {
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