describe("when adding ready callback and it is ready", function () {
    var readyCalled = false;

    beforeEach(function () {
        Bifrost.configure.onReady();
        Bifrost.configure.ready(function () {
            readyCalled = true;
        });
    });

    afterEach(function () {
        Bifrost.configure.reset();
    });


    it("should call the ready callback", function () {
        expect(readyCalled).toBe(true);
    });
});

