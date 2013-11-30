Bifrost.namespace("Bifrost.interaction", {
    EventTrigger: Bifrost.interaction.Trigger.extend(function () {
        var self = this;

        this.eventName = null;

        function throwIfEventNameIsNotSet(trigger) {
            if (!trigger.eventName) throw "EventName is not set for trigger";
        }

        this.initialize = function (element) {
            throwIfEventNameIsNotSet(this);

            var actualEventName = "on" + self.eventName;
            if (element[actualEventName] == null || typeof element[actualEventName] == "function") {
                var originalEventHandler = element[actualEventName];
                element[actualEventName] = function (e) {
                    if (originalEventHandler) originalEventHandler(e);
                    
                    self.signal();
                };
            }

        };
    })
});