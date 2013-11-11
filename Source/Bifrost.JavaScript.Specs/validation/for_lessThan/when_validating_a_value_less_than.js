describe("when validating a value less than", function () {
    var validator = Bifrost.validation.lessThan.create({ options: { value: 3 } });
    var result = validator.validate("2");

    it("should be valid", function () {
        expect(result).toBe(true);
    });
});
