describe("when extending with object literal", function() {
	var exception;
	
	try {
		Bifrost.Type.extend({});		
	} catch(e) {
		exception = e;
	}
	
	it("should throw object literal not allowed exception", function() {
		expect(exception instanceof Bifrost.ObjectLiteralNotAllowed).toBeTruthy();
	});
});
