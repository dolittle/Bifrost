describe("when value to be validated is null", function () {
    var validator = Bifrost.validation.email.create({ options: {} });
    var result = validator.validate(null);

    it("should be false", function () {
        expect(result).toBe(false);
    });
});
