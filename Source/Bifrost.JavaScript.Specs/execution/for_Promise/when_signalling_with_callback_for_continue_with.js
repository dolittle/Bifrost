describe("when signalling with callback for continue with", function() {
	var promise = Bifrost.execution.Promise.create();
	var continued = false;

	promise.continueWith(function() {
		continued = true;
	});

	promise.signal();

	it("should continue", function() {
		expect(continued).toBe(true);
	});
});