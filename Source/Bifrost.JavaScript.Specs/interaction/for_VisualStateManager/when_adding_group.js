describe("when adding group", function() {
	var group = { some : "group"};

	var stateManager = Bifrost.interaction.VisualStateManager.create();
	stateManager.addGroup(group);

	it("should have one group", function() {
		expect(stateManager.groups().length).toBe(1);
	});

	it("should have the added group", function() {
		expect(stateManager.groups()[0]).toBe(group);
	});
});