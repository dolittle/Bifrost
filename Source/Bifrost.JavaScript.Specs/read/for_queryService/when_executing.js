describe("when executing", sinon.test(function () {

    var server = sinon.fakeServer.create();

    var expectedData = [{ something: 'hello' }, { something: 'world'}];
    var requestBody = null;
    var descriptor = null;
    server.respondWith("POST", "/Bifrost/Query/Execute", function (xhr) {
        requestBody = $.parseJSON(xhr.requestBody);
        descriptor = $.parseJSON(requestBody.descriptor);
        xhr.respond(200, { "Content-Type": "application/json" }, JSON.stringify(expectedData));
    });

    var query = {
        name: "SomeQuery",
        integer: ko.observable(42),
        string: ko.observable("Hello world")
    };

    var receivedData = null;
    var queryService = Bifrost.read.queryService.create();
    var promise = queryService.execute(query);
    promise.continueWith(function (data) {
        receivedData = data;
    });

    server.respond();

    it("should return a promise", function () {
        expect(promise instanceof Bifrost.execution.Promise).toBe(true);
    });

    it("should continue with the data from the server", function () {
        expect(receivedData).toEqual(expectedData);
    });

    it("should send the name of query in the descriptor", function () {
        expect(descriptor.nameOfQuery).toEqual(query.name);
    });

    it("should add the integer parameter to the request", function () {
        expect(descriptor.parameters.integer).toBe(42);
    });

    it("should add the string parameter to the request", function () {
        expect(descriptor.parameters.string).toBe("Hello world");
    });
}));
