describe("when not providing a comparison value", function () {
    var exception = null;
    try {
        var validator = Bifrost.validation.greaterThan.create({ options: {} });
        validator.validate("1234");
    } catch (e) {
        exception = e;
    }

    it("should throw an exception", function () {
        expect(exception instanceof Bifrost.validation.OptionsNotDefined).toBe(true);
    });
});