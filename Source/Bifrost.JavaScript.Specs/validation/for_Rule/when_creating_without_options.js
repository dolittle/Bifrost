describe("when creating without options", function () {
    it("should not fail", function () {
        expect(function () { Bifrost.validation.Rule.create() }).not.toThrow();
    });
});