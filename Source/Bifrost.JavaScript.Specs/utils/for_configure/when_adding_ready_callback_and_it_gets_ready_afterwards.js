describe("when adding ready callback and it gets ready afterwards", function () {
    var readyCalled = false;

    beforeEach(function () {
        Bifrost.configure.ready(function () {
            readyCalled = true;
        });
        Bifrost.configure.onReady();
    });

    afterEach(function () {
        Bifrost.configure.reset();
    });

    it("should call the ready callback", function () {
        expect(readyCalled).toBe(true);
    });
});

