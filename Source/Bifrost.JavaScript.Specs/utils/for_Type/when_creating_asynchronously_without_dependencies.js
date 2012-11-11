describe("when creating asynchronously without dependencies", function() {
	var type = Bifrost.Type.define(function() {
		this.something = "Hello";
	});

	var result = null;
	type.beginCreate().continueWith(function(nextPromise, parameter) {
		result = parameter;
	});

	it("should create an instance", function() {
		expect(result.something).toBe("Hello");
	});
});