describe("when adding action", function () {

    var action = {
        someAction: 42
    };

    var trigger = Bifrost.interaction.Trigger.create();
    trigger.addAction(action);

    it("should have one action", function () {
        expect(trigger.actions.length).toBe(1);
    });

    it("should have the added action", function () {
        expect(trigger.actions[0]).toBe(action);
    });
});