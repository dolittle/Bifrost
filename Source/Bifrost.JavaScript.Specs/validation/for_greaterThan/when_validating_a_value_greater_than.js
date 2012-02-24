describe("when validating a value greater than", function () {
    it("should return false", function () {
        var value = Bifrost.validation.ruleHandlers.greaterThan.validate("4", { value: 3 });
        expect(value).toBeTruthy();
    });
});
