describe("when defining with null parameter", function() {
	var exception;
	
	try {
		Bifrost.Class(null);
	} catch(e) {
		exception = e;
	}
	
	it("should throw missing class definition exception", function() {
		expect(exception instanceof Bifrost.MissingTypeDefinition).toBeTruthy();
	});
});