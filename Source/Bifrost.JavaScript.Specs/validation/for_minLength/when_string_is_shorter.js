describe("when string is shorter", function () {
    it("should return false", function () {
        var result = Bifrost.validation.ruleHandlers.minLength.validate("1234", { length: 5 });
        expect(result).toBeFalsy();
    });
});