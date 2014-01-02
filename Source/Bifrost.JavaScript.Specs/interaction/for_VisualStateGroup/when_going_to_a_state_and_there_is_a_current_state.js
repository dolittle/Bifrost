describe("when going to a state and there is a current state", function() {

	var stateBefore = null;
	var stateAfter = null;

	var dispatcher = {};
	var group = Bifrost.interaction.VisualStateGroup.create({dispatcher: dispatcher});

	dispatcher.schedule = function(milliseconds, callback) {
		stateBefore = group.currentState();
		callback();
		stateAfter = group.currentState();
	};


	var firstState = {
		name: "something Else",
		exit: sinon.stub()
	};

	var secondState = {
		name : "something",
		enter: sinon.stub()
	};

	var namingRoot = { some: "namingRoot" };

	group.defaultDuration = Bifrost.TimeSpan.fromMilliseconds(100);
	group.states.push(firstState);
	group.states.push(secondState);

	group.currentState(firstState);
	group.goTo(namingRoot, "something");

	it("should not switch current state before it has transitioned", function() {
		expect(stateBefore).toBe(firstState);
	});
	it("should switch current state when it has transitioned", function() {
		expect(stateAfter).toBe(secondState);
	});

	it("should tell the current state to exit", function() {
		expect(firstState.exit.calledWith(namingRoot, group.defaultDuration)).toBe(true);
	});

	it("should tell the state to enter", function() {
		expect(secondState.enter.calledWith(namingRoot, group.defaultDuration)).toBe(true);
	});
});