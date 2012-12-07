describe("when publishing a message with a subscriber subscribing to different message", function() {
	var subscriberCalled = false;
	
	function FirstMessage() {
	}
	function SecondMessage() {
	}
	
	function subscriber(message) {
		subscriberCalled = true;
	}
	
	Bifrost.messaging.messenger.subscribeTo("SecondMessage", subscriber);
	var message = new FirstMessage();
	Bifrost.messaging.messenger.publish(message);
	
	
	it("should not call the subscriber", function() {
		expect(subscriberCalled).toBe(false);
	});
});