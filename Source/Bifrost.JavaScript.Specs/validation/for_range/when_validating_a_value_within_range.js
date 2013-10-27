describe("when validating a value within range", function () {
    var validator = Bifrost.validation.range.create({ options: { min: 5, max: 10 } });
    var result = validator.validate("7");

    it("should be valid", function () {
        expect(result).toBe(true);
    });
});
