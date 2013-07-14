describe("when value to be validated is undefined", function () {
    it("should be false", function () {
        var value = Bifrost.validation.ruleHandlers.minLength.validate(undefined, { length: 3 });
        expect(value).toBeFalsy();
    });
});