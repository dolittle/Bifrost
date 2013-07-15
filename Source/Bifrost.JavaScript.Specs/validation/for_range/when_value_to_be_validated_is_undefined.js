describe("when value to be validated is undefined", function () {
    it("should be false", function () {
        var value = Bifrost.validation.ruleHandlers.range.validate(undefined, { min: 3, max: 10 });
        expect(value).toBeFalsy();
    });
});