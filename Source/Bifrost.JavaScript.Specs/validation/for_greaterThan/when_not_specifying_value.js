describe("when not specifying value", function () {
    it("should throw an exception", function () {
        try {
            Bifrost.validation.ruleHandlers.greaterThan.validate("1234", {});
        } catch (e) {
            expect(e instanceof Bifrost.validation.ValueNotSpecified).toBeTruthy();
        }
    });
});