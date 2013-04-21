describe("when validating a valid email", function () {
    it("should return true", function () {
        var result = Bifrost.validation.ruleHandlers.email.validate("something@somewhere.com");
        expect(result).toBe(true);
    });
});