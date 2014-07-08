describe("when parameter changes in query", function () {
    var observable;
    var callbackToCall;

    var adapter = null;

    beforeEach(function () {
        History = History || {};

        adapter = History.Adapter;

        History.Adapter = {
            bind: function(callback) {
                callback();
            },
            getState: sinon.stub().returns({
                url: "http://www.somewhere.com/#?something=hello"
            })

        };
        observable = ko.observableQueryParameter("something");
        callbackToCall();
    });

    afterEach(function () {
        History.Adapter = adapter;
    });


    it("should hold the value from the query parameter", function () {
        expect(observable()).toBe("hello");
    });
});