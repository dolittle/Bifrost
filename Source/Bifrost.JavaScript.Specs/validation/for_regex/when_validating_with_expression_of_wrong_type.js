describe("when validating with expression of wrong type", function () {
    it("should throw missing expression", function () {
        try {
            Bifrost.validation.ruleHandlers.regex.validate("1234", { expression: {}});
        } catch (e) {
            expect(e instanceof Bifrost.validation.NotAString).toBe(true);
        }
    });
});