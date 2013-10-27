describe("when value to be validated is undefined", function () {
    var exception = null;
    try {
        Bifrost.validation.ruleHandlers.greaterThan.validate(undefined, { value: 3 });
    } catch (e) {
        exception = e;
    }

    it("should throw an exception", function () {
        expect(exception instanceof Bifrost.validation.ValueNotSpecified).toBe(true);
    });
});