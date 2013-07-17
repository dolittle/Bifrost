describe("when getting data from a url", sinon.test(function () {
    var url = "/Somewhere/With?query=value";
    var fakeServer = sinon.fakeServer.create();
    var data = { something: 42 };
    var requestUrl;
    var response;

    fakeServer.respondWith("GET", /\w/, function (xhr) {
        requestUrl = xhr.url;
        
        xhr.respond(200, { "Content-Type":"application/json" }, '{"somethingElse":"43"}');
    });

    var server = Bifrost.server.create();
    var promise = server.get(url, data);
    promise.continueWith(function (result) {
        response = result;
    });

    fakeServer.respond();

    it("should return a promise", function () {
        expect(promise instanceof Bifrost.execution.Promise).toBe(true);
    });

    it("should send the data as part of the body", function () {
        expect(requestUrl.indexOf("something=42") >= 0).toBe(true);
    });


    it("should deserialize the data coming back", function () {
        expect(response).toEqual({ somethingElse: 43 });
    });
}));