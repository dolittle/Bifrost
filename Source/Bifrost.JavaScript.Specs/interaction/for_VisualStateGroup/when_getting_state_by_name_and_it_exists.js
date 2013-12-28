describe("when getting state by name and it exists", function() {

	var group = Bifrost.interaction.VisualStateGroup.create();
	var firstState = {
		name: "Something else"
	};
	var secondState = {
		name: "Something"
	};

	group.states.push(firstState);
	group.states.push(secondState);

	var state = group.getStateByName("Something");

	it("should return the state matching by name", function() {
		expect(state).toBe(secondState);
	});
});