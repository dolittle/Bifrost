Bifrost.namespace("Bifrost.messaging", {
    Messenger: Bifrost.Type.extend(function () {

        this.publish = function (topic, message) {
        };

        this.subscribeTo = function (topic, callback) {
        };
    })
});

Bifrost.messaging.Messenger.global = Bifrost.messaging.Messenger.create();

/*

Bifrost.messaging.messenger = (function() {
	var subscribers = [];
	
	return {
		publish: function(message) {
			var messageTypeName = message.constructor.name;
			if( subscribers.hasOwnProperty(messageTypeName)) {
				$.each(subscribers[messageTypeName].subscribers, function(index, item) {
					item(message);
				});
			}
		},
	
		subscribeTo: function(messageType, subscriber) {
			var subscribersByMessageType;
			
			if( subscribers.hasOwnProperty(messageType)) {
				subscribersByMessageType = subscribers[messageType];
			} else {
				subscribersByMessageType = {subscribers:[]};
				subscribers[messageType] = subscribersByMessageType;
			}
			
			subscribersByMessageType.subscribers.push(subscriber);
		}
	}
})();*/