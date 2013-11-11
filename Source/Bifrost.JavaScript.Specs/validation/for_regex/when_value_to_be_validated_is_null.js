describe("when value to be validated is null", function () {
    var validator = Bifrost.validation.regex.create({ options: { expression: "[abc]" } });
    var result = validator.validate(null)

    it("should not be valid", function () {
        expect(result).toBe(false);
    });
});
