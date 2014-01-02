describe("when adding transition", function() {

	var group = Bifrost.interaction.VisualStateGroup.create({dispatcher: {}});

	var transition = { some: "transition" };

	group.addTransition(transition);

	it("should have one transition", function() {
		expect(group.transitions().length).toBe(1);
	});

	it("should have the transition added", function() {
		expect(group.transitions()[0]).toBe(transition);
	});
});