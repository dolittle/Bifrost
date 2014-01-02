describe("when changing main value when it already has an observable", function () {
    var actualValue = ko.observable("42");
    var subscription = { dispose: sinon.stub() };
    actualValue.subscribe = sinon.stub().returns(subscription);

    var linked = ko.observable();
    linked.extend({ linked: {} });

    linked(actualValue);

    var newValue = ko.observable("43");
    
    linked(newValue);

    it("should dispose previous value", function () {
        expect(subscription.dispose.called).toBe(true);
    });
});