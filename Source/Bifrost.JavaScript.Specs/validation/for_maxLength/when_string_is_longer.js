describe("when string is longer", function () {
    it("should return false", function () {
        var result = Bifrost.validation.ruleHandlers.maxLength.validate("123456", { max: 5 });
        expect(result).toBeFalsy();
    });
});