describe("when continued with parameter", function () {
    var promise = Bifrost.execution.Promise.create();
    var parameter = null;
    var nextPromise = null;

    promise.continueWith(function (param, e) {
        parameter = param;
        nextPromise = e;
    });

    promise.signal("Hello");

    it("should get a promise as argument", function () {
        expect(nextPromise instanceof Bifrost.execution.Promise).toBe(true);
    });

    it("should be a new promise", function () {
        expect(nextPromise).not.toBe(promise);
    });

    it("should not be a signaled promise", function () {
        expect(nextPromise.signalled).toBe(false);
    });

    it("should get the parameter", function () {
        expect(parameter).toBe("Hello");
    });

});