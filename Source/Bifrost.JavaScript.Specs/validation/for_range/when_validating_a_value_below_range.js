describe("when validating a value below range", function () {
    it("should return false", function () {
        var value = Bifrost.validation.ruleHandlers.range.validate("4", { min: 5, max: 10 });
        expect(value).toBeFalsy();
    });
});
