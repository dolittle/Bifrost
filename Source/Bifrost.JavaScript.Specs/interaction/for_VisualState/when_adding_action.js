describe("when adding action", function() {
	var action = { some : "action" };
	var state = Bifrost.interaction.VisualState.create();
	state.addAction(action);

	it("should have one action", function() {
		expect(state.actions().length).toBe(1);
	});

	it("should have the added action", function() {
		expect(state.actions()[0]).toBe(action);
	});
});