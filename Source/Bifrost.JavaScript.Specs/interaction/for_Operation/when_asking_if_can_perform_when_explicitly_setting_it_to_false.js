describe("when asking if can perform when explicitly setting it to false", function() {

	var operation = Bifrost.interaction.Operation.create({
		region: {},
		context: {}
	});

	operation.canPerform(false);
	var canPerform = operation.canPerform();

	it("should return false", function() {
		expect(canPerform).toBe(false);
	});
});