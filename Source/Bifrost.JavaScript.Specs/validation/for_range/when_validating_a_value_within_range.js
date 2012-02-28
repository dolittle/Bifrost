describe("when validating a value within range", function () {
    it("should return true", function () {
        var value = Bifrost.validation.ruleHandlers.range.validate("7", { min: 5, max: 10 });
        expect(value).toBeTruthy();
    });
});
