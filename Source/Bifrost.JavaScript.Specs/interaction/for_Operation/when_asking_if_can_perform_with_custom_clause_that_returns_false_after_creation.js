describe("when asking if can perform with custom clause that returns false after creation", function() {

	var operation = Bifrost.interaction.Operation.create({
		region: {},
		context: {}
	});
	var canPerform = null;
	operation.canPerform.subscribe(function(newValue) {
		canPerform = newValue;
	});

	var canPerformWhen = ko.observable(true);;
	operation.canPerform.when(canPerformWhen);
	canPerformWhen(false);

	it("should result in canPerform observable trigger and result in not being able to perform", function() {
		expect(canPerform).toBe(false);
	});
});