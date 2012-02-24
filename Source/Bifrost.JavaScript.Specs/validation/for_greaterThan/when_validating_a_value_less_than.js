describe("when validating a value less than", function () {
    it("should return true", function () {
        var value = Bifrost.validation.ruleHandlers.greaterThan.validate("2", { value: 3 });
        expect(value).toBeFalsy();
    });
});
