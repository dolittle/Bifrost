describe("when value changes", function () {

    var pushedState;
    var pushedTitle;
    var pushedRequestString;
    var uriParameters;

    beforeEach(function () {
        sinon.stub(History, "getState", function () {
            return {
                title: "A Title",
                url: "http://www.somewhere.com/"
            }
        });

        sinon.stub(History, "pushState", function (state, title, requestString) {
            pushedState = state;
            pushedTitle = title;
            pushedRequestString = requestString;
            uriParameters = Bifrost.hashString.decode(requestString);
        });

        var observable = ko.observableQueryParameter("something");
        observable("Hello");
    });

    afterEach(function () {
        History.getState.restore();
        History.pushState.restore();
    });

    it("should have a new state pushed", function () {
        expect(pushedState.something).toBe("Hello");
    });

    it("should have request parameter set", function () {
        expect(uriParameters.something).toBe("Hello");
    });
});