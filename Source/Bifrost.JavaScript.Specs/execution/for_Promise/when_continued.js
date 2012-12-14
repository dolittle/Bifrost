describe("when continued", function() {
	var promise = Bifrost.execution.Promise.create();
	var nextPromise = null;

	promise.continueWith(function(e) {
		nextPromise = e;
	});

	promise.signal();

	it("should get a promise as argument", function() {
		expect(nextPromise).not.toBeUndefined();
	});

	it("should be a new promise", function() {
		expect(nextPromise).not.toBe(promise);
	});

	it("should not be a signaled promise", function() {
		expect(nextPromise.signalled).toBe(false);
	});
	
});