describe("when value to be validated is null", function () {
    it("should be false", function () {
        var value = Bifrost.validation.ruleHandlers.regex.validate(null, { expression: "[abc]" });
        expect(value).toBeFalsy();
    });
});
