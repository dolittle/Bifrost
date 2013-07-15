describe("when the value to be validated is not a string", function () {
    it("should throw an exception", function () {
        try {
            Bifrost.validation.ruleHandlers.email.validate({});
        } catch (e) {
            expect(e instanceof Bifrost.validation.NotAString).toBeTruthy();
        }
    });
});