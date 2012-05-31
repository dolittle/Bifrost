describe("when creating instance of defined exception", function() {
	Bifrost.Exception.define("SomeException");
	
	var instance = new SomeException();
	
	it("should have an instance of error type", function() {
		expect(instance instanceof Error).toBeTruthy();
	});
});