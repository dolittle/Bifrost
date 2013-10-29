describe("when validating without expression", function () {
    var exception = null;
    try {
        var validator = Bifrost.validation.regex.create({ options: { expression: null } });
        validator.validate("1234");
    } catch (e) {
        exception = e;
    }

    it("should throw not a string exception", function () {
        expect(exception instanceof Bifrost.validation.MissingExpression).toBe(true);
    });
});