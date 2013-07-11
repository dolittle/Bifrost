describe("when value to be validated is undefined", function () {  
    it("should throw an exception", function () {
        try {
            Bifrost.validation.ruleHandlers.lessThan.validate(undefined, { value: 3 });
        } catch (e) {
            expect(e instanceof Bifrost.validation.ValueNotSpecified).toBeTruthy();
        }
    });
});