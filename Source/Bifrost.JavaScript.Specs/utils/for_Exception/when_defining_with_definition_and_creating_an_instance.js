describe("when defining with definition and creating an instance", function() {
	var someValue = "Something";
	
	Bifrost.Exception.define("SomeException", "Default message", function(message) {
		this.someProperty = someValue;
	});
	
	var instance = new SomeException();
	
	it("should have the definitions property in it", function() {
		expect(instance.someProperty).toBe(someValue);
	});
});