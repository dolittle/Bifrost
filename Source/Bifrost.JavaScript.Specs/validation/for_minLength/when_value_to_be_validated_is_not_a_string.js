describe("when value to be validated is not a string", function () {
    it("should throw an exception", function () {
        expect(function () { Bifrost.validation.ruleHandlers.minLength.validate(2, { length: 3 }) }).toThrow();
        });
});