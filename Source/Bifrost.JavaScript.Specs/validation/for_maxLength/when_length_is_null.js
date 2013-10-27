describe("when not specifying length", function () {
    var exception = null;
    try {
        var validator = Bifrost.validation.maxLength.create({ options: { length: null } })
        validator.validate("1234")
    } catch (e) {
        exception = e;
    }

    it("should throw max not specified exception", function () {
        expect(exception instanceof Bifrost.validation.MaxNotSpecified).toBe(true);
    });
});