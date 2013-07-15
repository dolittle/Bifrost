describe("when value to be validated is null", function () {
    it("should be false", function () {
        var value = Bifrost.validation.ruleHandlers.email.validate(null);
        expect(value).toBeFalsy();
    });
});
