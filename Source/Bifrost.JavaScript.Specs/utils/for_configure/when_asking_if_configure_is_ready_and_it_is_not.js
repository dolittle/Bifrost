describe("when asking if configure is ready and it is not", function () {
    beforeEach(function () {
        Bifrost.configure.reset();
    });

    it("should return false", function () {
        expect(Bifrost.configure.isReady()).toBe(false);
    });
});