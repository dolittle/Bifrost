describe("when signalling with parameter", function() {
	var promise = Bifrost.execution.Promise.create();
	var parameter;

	promise.continueWith(function(p) {
		parameter = p;
	});

	promise.signal("Hello");

	it("should pass along the parameter", function() {
		expect(parameter).toBe("Hello");
	});
});