describe("when string is longer", function () {
    var validator = Bifrost.validation.minLength.create({ options: { length: 5 } });
    var result = validator.validate("123456");

    it("should be valid", function () {
        expect(result).toBe(true);
    });
});