describe("when observing global messenger and message is published", function () {

    var observable = null;
    beforeEach(function () {
        var observablesCallback;
        Bifrost.messaging = Bifrost.messaging || {};
        Bifrost.messaging.Messenger = {
            global: {
                publish: sinon.stub(),
                subscribeTo: function (message, callback) {
                    observablesCallback = callback;
                }
            }
        }
        observable = ko.observableMessage("something");
        observablesCallback("Hello");
    });


    it("should hold the value from the message", function () {
        expect(observable()).toBe("Hello");
    });

    it("should not call publish after it has changed the observable", function () {
        expect(Bifrost.messaging.Messenger.global.publish.called).toBe(false);
    });
});