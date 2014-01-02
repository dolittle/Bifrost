describe("when checking if has state and it has not", function() {

	var group = Bifrost.interaction.VisualStateGroup.create({dispatcher: {}});

	var hasState = group.hasState("Something");

	it("should return false", function() {
		expect(hasState).toBe(false);
	});
});