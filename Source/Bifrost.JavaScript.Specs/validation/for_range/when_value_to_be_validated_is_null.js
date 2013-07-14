describe("when value to be validated is null", function () {
    it("should be false", function () {
        var value = Bifrost.validation.ruleHandlers.range.validate(null, { min: 3, max: 10 });
        expect(value).toBeFalsy();
    });
});
