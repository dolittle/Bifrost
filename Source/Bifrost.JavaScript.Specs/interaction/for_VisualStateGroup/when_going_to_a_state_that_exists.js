describe("when going to a state that exists", function() {

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

	it("should not switch current state before it has transitioned", function() {
		expect(stateBefore.name).toBe("null state");
	});
	it("should switch current state when it has transitioned", function() {
		expect(stateAfter).toBe(secondState);
	});

	it("should tell the state to enter", function() {
		expect(secondState.enter.calledWith(namingRoot, group.defaultDuration)).toBe(true);
	});
});