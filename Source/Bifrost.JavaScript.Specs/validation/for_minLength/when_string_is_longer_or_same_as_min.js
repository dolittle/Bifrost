describe("when string is longer or same as min", function () {
    it("should return true", function () {
        var result = Bifrost.validation.ruleHandlers.minLength.validate("12345", { length: 5 });
        expect(result).toBeTruthy();
    });
});