describe("when validating without at", function () {
    it("should return false", function () {
        var result = Bifrost.validation.ruleHandlers.email.validate("something");
        expect(result).toBe(false);
    });
});