describe("when value to be validated is not a string", function () {
    it("should throw an exception", function () {
        try {
            Bifrost.validation.ruleHandlers.regex.validate("katt", { expression: "[abc]" });
        } catch (e) {
            expect(e instanceof Bifrost.validation.NotANumber).toBeTruthy();
        }
    });
});