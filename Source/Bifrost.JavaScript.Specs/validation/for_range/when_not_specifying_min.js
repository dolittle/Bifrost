describe("when not specifying min", function () {
    it("should throw an exception", function () {
        try {
            Bifrost.validation.ruleHandlers.range.validate("1234", { max: 5 });
        } catch (e) {
            expect(e instanceof Bifrost.validation.MinNotSpecified).toBeTruthy();
        }
    });
});