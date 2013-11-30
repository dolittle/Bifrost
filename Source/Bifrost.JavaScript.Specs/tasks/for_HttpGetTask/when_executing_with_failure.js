describe("when executing with failure", function () {

    var url = "somewhere/else";
    var payload = { something: 42 };
    var expectedError = "Something went wrong";

    var server = {
        get: sinon.mock().withArgs(url, payload).returns({
            continueWith: function (callback) {
                
                return this;
            },
            onFail: function (callback) {
                callback(expectedError);
                return this;
            }
        })
    };

    var error = null;
    
    var task = Bifrost.tasks.HttpGetTask.create({
        url: url,
        payload: payload,
        server: server
    });
    
    task.execute().onFail(function (e) {
        error = e;
    });
    
    it("should fail with the expected error", function () {
        expect(error).toBe(expectedError);
    });
});