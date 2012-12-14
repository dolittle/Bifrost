describe("when publishing a message with one subscriber", function () {
    var wasMessageReceived = false;
    var messageReceived;

    function received(message) {
        wasMessageReceived = true;
        messageReceived = message;
    }

    var messenger = Bifrost.messaging.Messenger.create();
    messenger.subscribeTo("SimpleMessage", received);
    messenger.publish("SimpleMessage", "Hello")

    it("should call the subscriber", function () {
        expect(wasMessageReceived).toBe(true);
    });

    it("should pass along the message to the subscriber", function () {
        expect(messageReceived).toBe("Hello");
    });
});