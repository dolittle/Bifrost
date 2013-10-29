describe("when value to be validated is undefined", function () {
    var validator = Bifrost.validation.regex.create({ options: { expression: "[abc]" } });
    var result = validator.validate()

    it("should not be valid", function () {
        expect(result).toBe(false);
    });
});