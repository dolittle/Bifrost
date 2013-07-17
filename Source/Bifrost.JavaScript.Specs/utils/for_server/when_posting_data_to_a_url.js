describe("when posting data to a url", sinon.test(function () {
    var url = "/Somewhere/With?query=value";
    var fakeServer = sinon.fakeServer.create();
    var data = { something: 42 };
    var requestBody;
    var response;

    fakeServer.respondWith("POST", /\w/, function (xhr) {
        requestBody = xhr.requestBody;
        xhr.respond(200, { "Content-Type":"application/json" }, '{"somethingElse":"43"}');
    });

    var server = Bifrost.server.create();
    var promise = server.post(url, data);
    promise.continueWith(function (result) {
        response = result;
    });

    fakeServer.respond();

    it("should return a promise", function () {
        expect(promise instanceof Bifrost.execution.Promise).toBe(true);
    });

    it("should send the data as part of the body", function () {
        expect(requestBody).toEqual('{"something":"42"}');
    });


    it("should deserialize the data coming back", function () {
        expect(response).toEqual({ somethingElse: 43 });
    });
}));