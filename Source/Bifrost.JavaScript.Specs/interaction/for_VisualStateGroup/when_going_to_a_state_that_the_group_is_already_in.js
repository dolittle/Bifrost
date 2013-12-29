describe("when going to a state that the group is already in", function() {

	var group = Bifrost.interaction.VisualStateGroup.create();

	var firstState = {
		name: "something Else"
	};

	var secondState = {
		name : "something",
		enter: sinon.stub()
	};

	var namingRoot = { some: "namingRoot" };

	group.defaultDuration = Bifrost.TimeSpan.fromMilliseconds(100);
	group.states.push(firstState);
	group.states.push(secondState);
	group.currentState(secondState);

	group.goTo(namingRoot, "something");

	it("should switch current state", function() {
		expect(group.currentState()).toBe(secondState);
	});

	it("should not tell the state to enter", function() {
		expect(secondState.enter.called).toBe(false);
	});
});