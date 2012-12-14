describe("when publishing a message with a subscriber subscribing to different message", function () {
    var subscriberCalled = false;

    function subscriber(message) {
        subscriberCalled = true;
    }

    var messenger = Bifrost.messaging.Messenger.create();
    messenger.subscribeTo("SecondMessage", subscriber);
    messenger.publish("FirstMessage", "Hello");

    it("should not call the subscriber", function () {
        expect(subscriberCalled).toBe(false);
    });
});