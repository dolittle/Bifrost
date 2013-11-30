describe("when failing with callback defined ", function () {
    var error = null;
    var errorCalledWith = null;

    var promise = Bifrost.execution.Promise.create();
    promise.onFail(function (e) {
        errorCalledWith = e;
    });
    promise.fail("hello");

    it("should call the failed called with error", function () {
        expect(errorCalledWith).toBe("hello");
    });
});