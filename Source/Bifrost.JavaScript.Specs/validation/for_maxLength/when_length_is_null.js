describe("when not specifying length", function () {
    it("should throw an exception", function () {
        expect(function () { Bifrost.validation.ruleHandlers.maxLength.validate("1234", { length: null }) }).toThrow();
    });
});