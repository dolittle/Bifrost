describe("when failing", function () {
    var error = null;
    try {
        var promise = Bifrost.execution.Promise.create();
        promise.fail("hello");
    } catch (e) {
        error = e;
    }

    it("should throw the error given", function () {
        expect(error).toBe("hello");
    });
});