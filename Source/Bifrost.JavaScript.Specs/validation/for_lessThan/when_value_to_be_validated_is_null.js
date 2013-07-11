describe("when value to be validated is null", function () {
    it("should be false", function () {
        var value = Bifrost.validation.ruleHandlers.lessThan.validate(null, { value: 3 });
        expect(value).toBeFalsy();
    });
});
