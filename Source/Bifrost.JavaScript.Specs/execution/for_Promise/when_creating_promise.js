describe("when creating promise", function() {
	var promise = Bifrost.execution.Promise.create();

	it("should return an instance", function() {
		expect(promise).not.toBeNull();
	});
});