describe("when not specifying max", function () {
    it("should throw an exception", function () {
        expect(function () { Bifrost.validation.ruleHandlers.maxLength.validate("1234") }).toThrow();
    });
});