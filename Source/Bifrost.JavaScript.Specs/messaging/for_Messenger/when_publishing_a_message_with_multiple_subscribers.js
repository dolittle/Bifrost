describe("when publishing a message with multiple subscribers", function () {
    var firstSubscriberCalled = false;
    var secondSubscriberCalled = false;
    var thirdSubscriberCalled = false;
    function firstSubscriber(message) {
        firstSubscriberCalled = true;
    }
    function secondSubscriber(message) {
        secondSubscriberCalled = true;
    }
    function thirdSubscriber(message) {
        thirdSubscriberCalled = true;
    }

    var messenger = Bifrost.messaging.Messenger.create();
    messenger.subscribeTo("SimpleMessage", firstSubscriber);
    messenger.subscribeTo("SimpleMessage", secondSubscriber);
    messenger.subscribeTo("SimpleMessage", thirdSubscriber);
    messenger.publish("SimpleMessage", "Hello")

    it("should call all the subscribers", function () {
        var allSubscribersCalled = firstSubscriberCalled && secondSubscriberCalled && thirdSubscriberCalled;
        expect(allSubscribersCalled).toBe(true);
    });
});