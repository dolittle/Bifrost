describe("when validating a value above range", function () {
    it("should return false", function () {
        var value = Bifrost.validation.ruleHandlers.range.validate("11", { min: 5, max: 10 });
        expect(value).toBeFalsy();
    });
});
