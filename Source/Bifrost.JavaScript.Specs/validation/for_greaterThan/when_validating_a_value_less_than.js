describe("when validating a value less than", function () {
    var validator = Bifrost.validation.greaterThan.create({ options: { value: 3 } });
    var result = validator.validate("2");

    it("should be false", function () {
        expect(result).toBe(false);
    });
});
