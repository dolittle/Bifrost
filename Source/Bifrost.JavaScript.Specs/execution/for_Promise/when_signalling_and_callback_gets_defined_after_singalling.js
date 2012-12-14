describe("when signalling and callback gets defined after singalling", function() {
	var promise = Bifrost.execution.Promise.create();
	var continued = false;

	promise.signal();

	promise.continueWith(function() {
		continued = true;
	});

	it("should continue", function() {
		expect(continued).toBe(true);
	});
});