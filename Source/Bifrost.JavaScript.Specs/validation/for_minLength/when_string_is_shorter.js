describe("when string is shorter", function () {
    it("should return true", function () {
        var result = Bifrost.validation.ruleHandlers.minLength.validate("12345", { length: 5 });
        expect(result).toBeFalsy();
    });
});