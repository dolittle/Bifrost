describe("when default value is specified and query parameter dies not exist", function () {
    var observable;

    beforeEach(function () {
        sinon.stub(History.Adapter, "bind", function (scope, event, callback) {
            callback();
        });
        sinon.stub(History, "getState", function () {
            return {
                url: "http://www.somewhere.com/"
            }
        });
        observable = ko.observableQueryParameter("something","DefaultValue");
    });

    afterEach(function () {
        History.Adapter.bind.restore();
        History.getState.restore();
    });

    it("should hold the value from the query parameter", function () {
        expect(observable()).toBe("DefaultValue");
    });
});