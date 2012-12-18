describe("when asking if configure is ready and it is", function () {
    beforeEach(function () {
        Bifrost.configure.onReady();
    });
    afterEach(function () {
        Bifrost.configure.reset();
    });
    it("should return true", function () {
        expect(Bifrost.configure.isReady()).toBe(true);
    });
});