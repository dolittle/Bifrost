describe("when setting value to observable and it changes", function () {
    var actualValue = ko.observable("42");
    var linked = ko.observable();
    linked.extend({ linked: {} });

    linked(actualValue);

    var subscriber = sinon.stub();
    linked.subscribe(subscriber);

    actualValue("43");

    it("should notify subscribers", function () {
        expect(subscriber.called).toBe(true);
    });
});