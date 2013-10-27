describe("when string is longer", function () {
    var validator = Bifrost.validation.maxLength.create({ options: { length: 5 } });
    var result = validator.validate("123456");

    it("should not be valid", function () {
        expect(result).toBe(false);
    });
});