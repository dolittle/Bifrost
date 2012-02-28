describe("when publishing a message with multiple subscribers", function() {
	var firstSubscriberCalled = false;
	var secondSubscriberCalled = false;
	var thirdSubscriberCalled = false;
	function SimpleMessage(something) {
		this.something = something;
	}
	
	function firstSubscriber(message) {
		firstSubscriberCalled = true;
	}
	function secondSubscriber(message) {
		secondSubscriberCalled = true;
	}
	function thirdSubscriber(message) {
		thirdSubscriberCalled = true;
	}
	
	Bifrost.messaging.messenger.subscribeTo("SimpleMessage", firstSubscriber);
	Bifrost.messaging.messenger.subscribeTo("SimpleMessage", secondSubscriber);
	Bifrost.messaging.messenger.subscribeTo("SimpleMessage", thirdSubscriber);
	var message = new SimpleMessage("Hello");
	Bifrost.messaging.messenger.publish(message)
	
	it("should call all the subscribers", function() {
		var allSubscribersCalled = firstSubscriberCalled && secondSubscriberCalled && thirdSubscriberCalled;
		expect(allSubscribersCalled).toBe(true);
	});
});