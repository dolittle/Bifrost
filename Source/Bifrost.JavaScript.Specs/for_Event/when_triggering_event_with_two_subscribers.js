describe("when triggering event with two subscribers", function () {

    var event = Bifrost.Event.create();

    var firstSubscriber = sinon.stub();
    var secondSubscriber = sinon.stub();
    var data = { some: "data" };

    event.subscribe(firstSubscriber);
    event.subscribe(secondSubscriber);

    event.trigger(data)

    it("should trigger first subscriber", function () {
        expect(firstSubscriber.calledWith(data)).toBe(true);
    });

    it("should trigger second subscriber", function () {
        expect(secondSubscriber.calledWith(data)).toBe(true);
    });

});
