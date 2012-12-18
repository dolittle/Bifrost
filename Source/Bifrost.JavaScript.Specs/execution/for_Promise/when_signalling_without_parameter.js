describe("when signalling without parameter", function () {
    var promise = Bifrost.execution.Promise.create();
    var nextPromise;

    promise.continueWith(function (p) {
        nextPromise = p;
    });

    promise.signal();

    it("should pass along next promise", function () {
        expect(nextPromise instanceof Bifrost.execution.Promise).toBe(true);
    });
});