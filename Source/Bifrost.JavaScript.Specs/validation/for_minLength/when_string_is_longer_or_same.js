describe("when string is longer", function () {
    it("should return false", function () {
        var result = Bifrost.validation.ruleHandlers.minLength.validate("123456", { length: 5 });
        expect(result).toBeTruthy();
    });
});