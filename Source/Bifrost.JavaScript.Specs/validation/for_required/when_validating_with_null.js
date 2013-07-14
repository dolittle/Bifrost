describe("when validating with undefined", function () {
    it("should return false", function () {
        var result = Bifrost.validation.ruleHandlers.required.validate(null);
        expect(result).toBeFalsy();
    });
});