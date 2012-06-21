describe("when publishing a message with one subscriber", function() {
	var wasMessageReceived = false;
	var messageReceived;
	function SimpleMessage(something) {
		this.something = something;
	}
	
	function received(message) {
		wasMessageReceived = true;		
		messageReceived = message;
	}
	
	Bifrost.messaging.messenger.subscribeTo(SimpleMessage, received);
	var message = new SimpleMessage("Hello");
	Bifrost.messaging.messenger.publish(message)
	
	it("should call the subscriber", function() {
		expect(wasMessageReceived).toBe(true);
	});
	
	it("should pass along the message to the subscriber", function() {
		expect(messageReceived.something).toBe(message.something);
	});
});