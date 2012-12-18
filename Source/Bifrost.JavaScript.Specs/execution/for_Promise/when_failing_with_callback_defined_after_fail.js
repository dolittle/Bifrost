describe("when failing with callback defined ", function () {
    var error = null;
    var errorCalledWith = null;
    try {
        var promise = Bifrost.execution.Promise.create();
        promise.onFail(function (e) {
            errorCalledWith = e;
        });
        promise.fail("hello");
    } catch (e) {
        error = e;
    }

    it("should throw the error given", function () {
        expect(error).toBe("hello");
    });

    it("should call the failed called with error", function () {
        expect(errorCalledWith).toBe("hello");
    });
});