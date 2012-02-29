describe("when validating and it should match", function () {
    var result = Bifrost.validation.ruleHandlers.regex.validate("abcd", { expression: "[abc]" })
    it("should match", function () {
        expect(result).toBe(true);
    });
});