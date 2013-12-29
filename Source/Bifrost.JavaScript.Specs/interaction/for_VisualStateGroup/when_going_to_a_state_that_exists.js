describe("when going to a state that exists", function() {

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

	group.goTo(namingRoot, "something");

	it("should switch current state", function() {
		expect(group.currentState()).toBe(secondState);
	});

	it("should tell the state to enter", function() {
		expect(secondState.enter.calledWith(namingRoot, group.defaultDuration)).toBe(true);
	});
});