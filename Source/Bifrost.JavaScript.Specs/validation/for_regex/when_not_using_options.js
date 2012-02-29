describe("when not using options", function () {
    it("should throw an exception", function () {
        try {
            Bifrost.validation.ruleHandlers.regex.validate("1234");
        } catch (e) {
            expect(e instanceof Bifrost.validation.OptionsNotDefined).toBe(true);
        }
    });
});