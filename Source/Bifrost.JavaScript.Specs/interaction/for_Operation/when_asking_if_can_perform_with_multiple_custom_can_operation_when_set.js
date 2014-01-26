describe("when asking if can perform with multiple custom can operation when set", function() {

	var operation = Bifrost.interaction.Operation.create({
		region: {},
		context: {}
	});

	var firstCanPerformWhen = sinon.stub();
	var secondCanPerformWhen = sinon.stub();
	var canPerform = operation.canPerform.when(firstCanPerformWhen).when(secondCanPerformWhen);

	it("should ask the first can perform when given", function() {
		expect(firstCanPerformWhen.called).toBe(true);
	});

	it("should ask the second can perform when given", function() {
		expect(firstCanPerformWhen.called).toBe(true);
	});
});