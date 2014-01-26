describe("when asking if can perform without custom can operation when set", function() {

	var operation = Bifrost.interaction.Operation.create({
		region: {},
		context: {}
	});

	var canPerform = operation.canPerform();

	it("should return true when asking if it can perform", function() {
		expect(canPerform).toBe(true);
	});
});