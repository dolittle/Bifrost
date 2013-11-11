describe("when value to be validated is undefined", function () {
    var validator = Bifrost.validation.minLength.create({ options: { length: 3 } })
    var result = validator.validate(undefined)

    it("should be invalid", function () {
        expect(result).toBe(false);
    });
});