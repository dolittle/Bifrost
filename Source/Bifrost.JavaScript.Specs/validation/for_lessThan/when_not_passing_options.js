describe("when not passing options", function () {
	var exception;
	
    try {
        Bifrost.validation.ruleHandlers.lessThan.validate("1234");
    } catch (e) {
		exception = e;
    }
	
    it("should throw options not defined", function () {
        expect(exception instanceof Bifrost.validation.OptionsNotDefined).toBeTruthy();
    });
});