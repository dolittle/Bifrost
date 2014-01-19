describe("when default value is specified and query parameter exist as well", function () {
    Bifrost.navigation.navigationManager = {
        getCurrentLocation: sinon.stub().returns(Bifrost.Uri.create("http://www.somewhere.com?something=42"))
    };
    var observable = ko.observableQueryParameter("something", "DefaultValue");

    it("should hold the value from the query parameter", function () {
        expect(observable()).toBe(42);
    });
});