describe("when value to be validated is not a number", function () {
    it("should throw an exception", function () {
        try {
            Bifrost.validation.ruleHandlers.greaterThan.validate("Joe", { value: 3 });
        } catch (e) {
            expect(e instanceof Bifrost.validation.NotANumber).toBeTruthy();
        }
    });
});