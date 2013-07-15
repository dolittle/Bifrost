describe("when min is null", function () {
    it("should throw an exception", function () {
        try {
            Bifrost.validation.ruleHandlers.range.validate("1234", { min: null, max: 5 });
        } catch (e) {
            expect(e instanceof Bifrost.validation.MinNotSpecified).toBeTruthy();
        }
    });
});