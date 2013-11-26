describe("when signalling with one action", function () {
    var firstAction = {
        perform: sinon.stub()
    };
    var secondAction = {
        perform: sinon.stub()
    };

    var trigger = Bifrost.interaction.Trigger.create();
    trigger.addAction(firstAction);
    trigger.addAction(secondAction);

    trigger.signal();

    it("should perform the first action", function () {
        expect(firstAction.perform.called).toBe(true);
    });

    it("should perform the second action", function () {
        expect(secondAction.perform.called).toBe(true);
    });
});