describe("when default value is specified but query parameter exist as well", function () {
    var observable;

    beforeEach(function () {
        sinon.stub(window, "location", {
            toString: function () {
                return "http://www.somewhere.com/#?something=hello";
            }
        });


        /*
        sinon.stub(History.Adapter, "bind", function (scope, event, callback) {
            callback();
        });
        sinon.stub(History, "getState", function () {
            return {
                url: "http://www.somewhere.com/#?something=hello"
            }
        });*/
        observable = ko.observableQueryParameter("something");
    });

    afterEach(function () {
        window.location.restore();
        //History.Adapter.bind.restore();
        //History.getState.restore();
    });

    it("should hold the value from the query parameter", function () {
        expect(observable()).toBe("hello");
    });
});