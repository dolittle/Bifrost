describe("when changing state to known state", function() {

	var manager = Bifrost.interaction.VisualStateManager.create();

	manager.namingRoot = { some: "namingRoot" };

	var firstGroup = {
		hasState : sinon.stub().returns(false)
	};

	var state = {
		some: "state",
		enter: sinon.stub()

	};

	var secondGroup = {
		hasState : sinon.stub().returns(true),
		goTo: sinon.stub()

	};
	manager.groups.push(firstGroup);
	manager.groups.push(secondGroup);

	manager.goTo("someState");

	it("should enter the state for the group", function() {
		expect(secondGroup.goTo.calledWith(manager.namingRoot, "someState")).toBe(true);
	});
});