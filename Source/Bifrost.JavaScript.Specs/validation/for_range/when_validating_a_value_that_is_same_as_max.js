describe("when validating a value that is same as max", function () {
    it("should return true", function () {
        var value = Bifrost.validation.ruleHandlers.range.validate("10", { min: 5, max: 10 });
        expect(value).toBeTruthy();
    });
});
