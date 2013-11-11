describe("when validating with undefined", function () {
    var validator = Bifrost.validation.required.create({ options: {} });
    var result = validator.validate(null);

    it("should not be valid", function () {
        expect(result).toBe(false);
    });
});