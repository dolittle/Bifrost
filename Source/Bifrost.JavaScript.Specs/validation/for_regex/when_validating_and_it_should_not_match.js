describe("when validating and it should not match", function () {
    var result = Bifrost.validation.ruleHandlers.regex.validate("1234", { expression: "[abc]" });
    it("should not match", function () {
        expect(result).toBe(false);
    });
});