describe("when entering with two actions", function() {

	var namingRoot = { some : "namingRoot" };
	var duration = Bifrost.TimeSpan.fromMilliseconds(20);
	var firstAction = {
		onEnter: sinon.stub()
	};
	var secondAction = {
		onEnter: sinon.stub()
	};
	var state = Bifrost.interaction.VisualState.create();
	state.actions.push(firstAction);
	state.actions.push(secondAction);
	state.enter(namingRoot, duration);

	it("should call the first actions on enter", function() {
		expect(firstAction.onEnter.calledWith(namingRoot, duration)).toBe(true);
	});

	it("should call the second actions on enter", function() {
		expect(secondAction.onEnter.calledWith(namingRoot, duration)).toBe(true);
	});
});