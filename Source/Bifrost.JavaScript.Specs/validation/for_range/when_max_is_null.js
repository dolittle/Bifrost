describe("when max is null", function () {
    it("should throw an exception", function () {
        try {
            Bifrost.validation.ruleHandlers.range.validate("1234", { min: 5, max: null });
        } catch (e) {
            expect(e instanceof Bifrost.validation.MaxNotSpecified).toBeTruthy();
        }
    });
});