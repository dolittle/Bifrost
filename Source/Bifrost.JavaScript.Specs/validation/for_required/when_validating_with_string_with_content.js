describe("when validating with string with content", function () {
    it("should return true", function () {
        var result = Bifrost.validation.ruleHandlers.required.validate("something");
        expect(result).toBeTruthy();
    });
});