describe("when validating a value that is same as min", function () {
    it("should return true", function () {
        var value = Bifrost.validation.ruleHandlers.range.validate("5", { min: 5, max: 10 });
        expect(value).toBeTruthy();
    });
});
