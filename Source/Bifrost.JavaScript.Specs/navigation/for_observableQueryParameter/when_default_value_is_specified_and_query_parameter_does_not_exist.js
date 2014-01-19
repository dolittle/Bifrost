describe("when default value is specified but query parameter does not exist", function () {
    Bifrost.navigation.navigationManager = {
        getCurrentLocation: sinon.stub().returns(Bifrost.Uri.create("http://www.somewhere.com"))
    };
    var observable = ko.observableQueryParameter("something", "hello");
    it("should hold the value from the query parameter", function () {
        expect(observable()).toBe("hello");
    });
});