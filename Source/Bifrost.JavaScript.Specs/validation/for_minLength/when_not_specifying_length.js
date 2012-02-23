describe("when not specifying length", function () {
    it("should throw an exception", function () {
        expect(function () { Bifrost.validation.ruleHandlers.minLength.validate("1234") }).toThrow();
    });
});