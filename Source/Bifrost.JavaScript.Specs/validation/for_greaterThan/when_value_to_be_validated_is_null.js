describe("when value to be validated is null", function () {
    var validator = Bifrost.validation.greaterThan.create({ options: { value: 3 } });
    var result = validator.validate(null);

    it("should be false", function () {
        expect(result).toBe(false);
    });
});
