describe("when value to be validated is null", function () {
    var validator = Bifrost.validation.lessThan.create({ options: { value: 3 } });
    var result = validator.validate(null);

    it("should not be valid", function () {
        expect(result).toBe(false);
    });
});
