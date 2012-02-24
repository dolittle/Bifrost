describe("when validating a value greater than", function () {
    it("should return false", function () {
        var value = Bifrost.validation.ruleHandlers.lessThan.validate("4", { value: 3 });
        expect(value).toBeFalsy();
    });
});
