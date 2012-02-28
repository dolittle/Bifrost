describe("when string is shorter or same", function () {
    it("should return true", function () {
        var result = Bifrost.validation.ruleHandlers.maxLength.validate("12345", { length: 5 });
        expect(result).toBeTruthy();
    });
});