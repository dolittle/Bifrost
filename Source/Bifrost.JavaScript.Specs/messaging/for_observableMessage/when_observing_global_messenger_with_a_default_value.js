describe("when observing global messenger with a default value", function () {
    var observable = null;
    beforeEach(function () {
        Bifrost.messaging = Bifrost.messaging || {};
        Bifrost.messaging.Messenger = {
            global: {
                publish: sinon.stub(),
                subscribeTo: function (message, callback) {
                }
            }
        }
        observable = ko.observableMessage("something");
    });


    it("should not publish a message", function () {
        expect(Bifrost.messaging.Messenger.global.publish.called).toBe(false);
    });
});