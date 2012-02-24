describe("when validating a value less than", function () {
    it("should return true", function () {
        var value = Bifrost.validation.ruleHandlers.lessThan.validate("2", { value: 3 });
        expect(value).toBeTruthy();
    });
});
