describe("when validating a string", function () {
    it("should throw an exception", function () {
        try {
            Bifrost.validation.ruleHandlers.lessThan.validate("katt", { value: 5 });
        } catch (e) {
            expect(e instanceof Bifrost.validation.NotANumber).toBeTruthy();
        }
    });
});
