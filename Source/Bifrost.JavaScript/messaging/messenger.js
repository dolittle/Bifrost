Bifrost.namespace("Bifrost.messaging");
Bifrost.messaging.messenger = (function () {
    var funcNameRegex = /function\s+(.{1,})\s*\(/;
    var subscribers = [];

    return {
        publish: function (message) {
            var messageTypeName = "";
            if ("name" in message.constructor) {
                messageTypeName = message.constructor.name;
            } else {
                var regexResult = funcNameRegex.exec(message.constructor);
                if (regexResult && regexResult.length > 1) {
                    messageTypeName = regexResult[1];
                } else {
                    throw new Error("Message " + message + " is not an object with a name.\n");
                }
            }
            if (subscribers.hasOwnProperty(messageTypeName)) {
                $.each(subscribers[messageTypeName].subscribers, function (index, item) {
                    item(message);
                });
            }
        },

        subscribeTo: function (messageType, subscriber) {
            
            if(typeof messageType !== "string") {
                var regexResult = funcNameRegex.exec(messageType);
                if (regexResult && regexResult.length > 1) {
                    messageType = regexResult[1];
                } else {
                    throw new Error("MessageType " + messageType + " is not an object with a name.\n");
                }
            }


            var subscribersByMessageType;

            if (subscribers.hasOwnProperty(messageType)) {
                subscribersByMessageType = subscribers[messageType];
            } else {
                subscribersByMessageType = { subscribers: [] };
                subscribers[messageType] = subscribersByMessageType;
            }

            subscribersByMessageType.subscribers.push(subscriber);
        }
    }
})();