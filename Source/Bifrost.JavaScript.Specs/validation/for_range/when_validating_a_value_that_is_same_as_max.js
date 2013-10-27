describe("when validating a value that is same as max", function () {
    var validator = Bifrost.validation.range.create({ options: { min: 5, max: 10 } });
    var result = validator.validate("10");

    it("should be valid", function () {
        expect(result).toBe(true);
    });
});
