describe("when validating with string with content", function () {
    var validator = Bifrost.validation.required.create({ options: {} });
    var result = validator.validate("something");

    it("should be valid", function () {
        expect(result).toBe(true);
    });
});