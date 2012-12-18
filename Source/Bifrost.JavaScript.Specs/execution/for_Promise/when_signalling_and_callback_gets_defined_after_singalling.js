describe("when signalling and callback gets defined after singalling", function () {
    var promise = Bifrost.execution.Promise.create();
    var continued = false;
    var parameter = null;
    var nextPromise = null;

    promise.signal("Hello");

    promise.continueWith(function (param, np) {
        continued = true;
        parameter = param;
        nextPromise = np;
    });

    it("should continue", function () {
        expect(continued).toBe(true);
    });

    it("should have the parameter", function () {
        expect(parameter).toBe("Hello");
    });

    it("should be a new promise", function () {
        expect(nextPromise).not.toBe(promise);
    });


    it("should have the next promise", function () {
        expect(nextPromise instanceof Bifrost.execution.Promise).toBe(true);
    });
});