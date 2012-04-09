describe("when defining with wrong definition type", function() {
	var exception;

	try {
		Bifrost.Exception.define("Some name", "Default message", {});
	} catch( e ) {
		exception = e;
	}
	
	it("should throw definition must be function", function() {
		expect(exception instanceof Bifrost.DefinitionMustBeFunction).toBeTruthy();
	});
});