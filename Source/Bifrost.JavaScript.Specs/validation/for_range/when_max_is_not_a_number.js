describe("when not specifying max", function () {
    it("should throw an exception", function () {
        try {
            Bifrost.validation.ruleHandlers.range.validate("1234", { min: 5, max: "MAX" });
        } catch (e) {
            expect(e instanceof Bifrost.validation.NotANumber).toBeTruthy();
        }
    });
});