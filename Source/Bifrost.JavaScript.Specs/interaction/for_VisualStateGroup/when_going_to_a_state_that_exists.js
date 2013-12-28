describe("when going to a state that exists", function() {

	var group = Bifrost.interaction.VisualStateGroup.create();

	var firstState = {
		name: "something Else"
	};

	var secondState = {
		name : "something",
		enter: sinon.stub()
	};

	group.defaultDuration = Bifrost.TimeSpan.fromMilliseconds(100);
	group.states.push(firstState);
	group.states.push(secondState);

	group.goTo("something");

	it("should switch current state", function() {
		expect(group.currentState()).toBe(secondState);
	});

	it("should tell the state to enter", function() {
		expect(secondState.enter.calledWith(group.defaultDuration)).toBe(true);
	});
});