describe("when comparison value is null", function () {
    it("should throw an exception", function () {
        try {
            Bifrost.validation.ruleHandlers.greaterThan.validate("12345", { value: null });
        } catch (e) {
            expect(e instanceof Bifrost.validation.OptionsNotDefined).toBeTruthy();
        }
    });
});