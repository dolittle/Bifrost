describe("when creating asynchronously without dependencies", function() {
	var type = Bifrost.Type.extend(function() {
		this.something = "Hello";
	});

	var result = null;
	type.beginCreate().continueWith(function(parameter, nextPromise) {
		result = parameter;
	});

	it("should create an instance", function() {
		expect(result.something).toBe("Hello");
	});
});