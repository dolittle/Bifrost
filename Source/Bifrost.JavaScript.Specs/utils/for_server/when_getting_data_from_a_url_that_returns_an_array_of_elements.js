describe("when getting data from a url that returns an array of elements", sinon.test(function () {
    var url = "/Somewhere/With?query=value";
    var fakeServer = sinon.fakeServer.create();
    var data = { something: 42 };
    var response;

    $.support.cors = true;

    fakeServer.respondWith("GET", /\w/, function (xhr) {
        xhr.respond(200, { "Content-Type":"application/json" }, '[{"somethingElse":"43"},{"someStuff":"d44"}]');
    });

    var server = Bifrost.server.create();
    var promise = server.get(url, data);
    promise.continueWith(function (result) {
        response = result;
    });

    fakeServer.respond();
    
    it("should deserialize the data coming back", function () {
        expect(response).toEqual([
            { somethingElse: 43 },
            { someStuff: "d44" }
        ]);
    });
}));