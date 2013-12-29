describe("when exiting with two actions", function() {

	var namingRoot = { some : "namingRoot" };
	var duration = Bifrost.TimeSpan.fromMilliseconds(20);
	var firstAction = {
		onExit: sinon.stub()
	};
	var secondAction = {
		onExit: sinon.stub()
	};
	var state = Bifrost.interaction.VisualState.create();
	state.actions.push(firstAction);
	state.actions.push(secondAction);
	state.exit(namingRoot, duration);

	it("should call the first actions on exit", function() {
		expect(firstAction.onExit.calledWith(namingRoot, duration)).toBe(true);
	});

	it("should call the second actions on exit", function() {
		expect(secondAction.onExit.calledWith(namingRoot, duration)).toBe(true);
	});
});