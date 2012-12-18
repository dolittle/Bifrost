describe("when continued without parameter", function () {
    var promise = Bifrost.execution.Promise.create();
    var parameter = null;
    var nextPromise = null;

    promise.continueWith(function (e) {
        nextPromise = e;
    });

    promise.signal();

    it("should get a promise as argument", function () {
        expect(nextPromise instanceof Bifrost.execution.Promise).toBe(true);
    });

    it("should be a new promise", function () {
        expect(nextPromise).not.toBe(promise);
    });

    it("should not be a signaled promise", function () {
        expect(nextPromise.signalled).toBe(false);
    });
});