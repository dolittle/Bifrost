describe("when not providing a comparison value", function () {
    it("should throw an exception", function () {
        try {
            Bifrost.validation.ruleHandlers.lessThan.validate("1234", {});
        } catch (e) {
            expect(e instanceof Bifrost.validation.OptionsNotDefined).toBeTruthy();
        }
    });
});