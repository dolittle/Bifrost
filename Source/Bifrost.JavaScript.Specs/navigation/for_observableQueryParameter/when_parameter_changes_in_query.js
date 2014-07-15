describe("when parameter changes in query", function () {
    var observable;
    var callbackToCall = null;
    var historyAdapter = null;
    var historyGetState = null;

    beforeEach(function () {
        historyAdapter = History.Adapter;
        
        History.Adapter = {
            bind: function (scope, event, callback) {
                callbackToCall = callback;
            }
        };
        historyGetState = History.getState;
        History.getState = function () {
            return {
                url: "http://www.somewhere.com/#?something=hello"
            }
        };
        
        observable = ko.observableQueryParameter("something");
        callbackToCall();
    });

    afterEach(function () {
        History.Adapter = historyAdapter;
        History.getState = historyGetState;
    });

    it("should hold the value from the query parameter", function () {
        expect(observable()).toBe("hello");
    });
});