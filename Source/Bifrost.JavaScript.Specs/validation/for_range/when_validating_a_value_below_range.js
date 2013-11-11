describe("when validating a value below range", function () {
    var validator = Bifrost.validation.range.create({ options: { min: 5, max: 10 } });
    var result = validator.validate("4");

    it("should not be valid", function () {
        expect(result).toBe(false);
    });
});
