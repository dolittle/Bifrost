describe("creating instance without specifying message", function() {
	var message = "This is a message";
	Bifrost.Exception.define("SomeException", message);
	
	var instance = new SomeException();
	
	it("should have the default message", function() {
		expect(instance.message).toBe(message);
	});
});