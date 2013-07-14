describe("when value to be validated is undefined", function () {
    it("should be false", function () {
        var value = Bifrost.validation.ruleHandlers.email.validate(undefined);
        expect(value).toBeFalsy(); 
    });
});