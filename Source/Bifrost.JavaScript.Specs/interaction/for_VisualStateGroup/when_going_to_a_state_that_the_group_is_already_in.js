describe("when going to a state that the group is already in", function() {

	var dispatcher = { schedule: sinon.stub() };
	var group = Bifrost.interaction.VisualStateGroup.create({dispatcher: dispatcher});

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

	it("should not schedule a switch", function() {
		expect(dispatcher.schedule.called).toBe(false);
	});

	it("should not tell the state to enter", function() {
		expect(secondState.enter.called).toBe(false);
	});
});