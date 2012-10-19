describe("when defining without a parameter", function() {
	var exception;
	
	try {
		Bifrost.Type.define();
	} catch(e) {
		exception = e;
	}
	
	it("should throw missing class definition exception", function() {
		expect(exception instanceof Bifrost.MissingTypeDefinition).toBeTruthy();
	});
});