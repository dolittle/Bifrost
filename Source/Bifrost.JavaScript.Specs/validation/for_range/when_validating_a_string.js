describe("when validating a string", function () {
    it("should throw an exception", function () {
        try {
            Bifrost.validation.ruleHandlers.range.validate("katt", { min: 5, max: 10 });
        } catch (e) {
            expect(e instanceof Bifrost.validation.NotANumber).toBeTruthy();
        }
    });
});
