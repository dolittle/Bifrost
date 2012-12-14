describe("when parameter changes in query", function () {
    var observable;
    var callbackToCall;

    beforeEach(function () {
        sinon.stub(History.Adapter, "bind", function (scope, event, callback) {
            callbackToCall = callback;
        });
        sinon.stub(History, "getState", function () {
            return {
                url: "http://www.somewhere.com/#?something=hello"
            }
        });
        observable = ko.observableQueryParameter("something");
        callbackToCall();
    });

    afterEach(function () {
        History.Adapter.bind.restore();
        History.getState.restore();
    });


    it("should hold the value from the query parameter", function () {
        expect(observable()).toBe("hello");
    });
});