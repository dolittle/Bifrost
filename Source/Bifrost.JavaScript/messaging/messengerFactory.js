Bifrost.namespace("Bifrost.messaging", {
    messengerFactory: Bifrost.Singleton(function () {
        this.create = function () {
            var messenger = Bifrost.messaging.Messenger.create();
            return messenger;
        };

        this.global = function () {
            return Bifrost.messaging.Messenger.global;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.messengerFactory = Bifrost.messaging.messengerFactory;