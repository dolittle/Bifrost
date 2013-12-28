describe("when getting state by name and it does not exist", function() {

	var group = Bifrost.interaction.VisualStateGroup.create();
	var firstState = {
		name: "Something else"
	};
	var secondState = {
		name: "Something"
	};

	group.states.push(firstState);
	group.states.push(secondState);

	var state = group.getStateByName("Something that does not exist");

	it("should return null", function() {
		expect(state).toBe(null);
	});
});