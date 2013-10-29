describe("when validating a value that is same as min", function () {
    var validator = Bifrost.validation.range.create({ options: { min: 5, max: 10 } });
    var result = validator.validate("5");

    it("should be valid", function () {
        expect(result).toBe(true);
    });
});
