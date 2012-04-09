describe("throwing a defined exception", function() {
	var exceptionThrown;
	
	Bifrost.Exception.define("Something.SomeException");
	
	try {
		throw new Something.SomeException();
	} catch( e ) {
		exceptionThrown = e;
	}
	
	it("should have the same exception caught", function() {
		expect(exceptionThrown instanceof Something.SomeException).toBeTruthy();
	});
});