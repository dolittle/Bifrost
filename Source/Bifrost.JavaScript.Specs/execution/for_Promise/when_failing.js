describe("when failing", function () {
    var error = null;

    var promise = Bifrost.execution.Promise.create();
    promise.fail("hello");

    it("should set the promise in a failed state", function () {
        expect(promise.hasFailed).toBe(true);
    });
});