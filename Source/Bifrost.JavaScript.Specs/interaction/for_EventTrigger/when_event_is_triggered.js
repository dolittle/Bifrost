describe("when event is triggered", function () {
    var element = $("<button/>");
    var trigger = Bifrost.interaction.EventTrigger.create();
    trigger.eventName = "click";
    trigger.initialize(element[0]);
    trigger.signal = sinon.stub();

    element.trigger("click");

    it("should signal the trigger", function () {
        expect(trigger.signal.called).toBe(true);
    });
});
