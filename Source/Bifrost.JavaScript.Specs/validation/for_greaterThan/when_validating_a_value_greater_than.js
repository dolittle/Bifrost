describe("when validating a value greater than", function () {
    var validator = Bifrost.validation.greaterThan.create({ options: { value: 3 } });
    var result = validator.validate("4");

    it("should be true", function () {
        expect(result).toBe(true);
    });
});
