describe("when string is shorter or same as max", function () {
    it("should return true", function () {
        var result = Bifrost.validation.ruleHandlers.maxLength.validate("12345", { max: 5 });
        expect(result).toBeTruthy();
    });
});