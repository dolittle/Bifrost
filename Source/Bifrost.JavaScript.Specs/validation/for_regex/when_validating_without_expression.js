describe("when validating without expression", function () {
    it("should throw missing expression", function () {
        try {
            Bifrost.validation.ruleHandlers.regex.validate("1234", {});
        } catch (e) {
            expect(e instanceof Bifrost.validation.MissingExpression).toBe(true);
        }
    });
});