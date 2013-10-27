describe("when validating a value greater than", function () {
    var validator = Bifrost.validation.lessThan.create({ options: { value: 3 } });
    var result = validator.validate("4");

    it("should not be valid", function () {
        expect(result).toBe(false);
    });
});
