describe("when initializing validation service", sinon.test(function () {
    
    
    it("should have a recursivlyExtendProperties function", function () {
        expect(Bifrost.validation.validationService.recursivlyExtendProperties).toBeDefined();
    });
    it("should have a recursivlyApplyRules function", function () {
        expect(Bifrost.validation.validationService.recursivlyApplyRules).toBeDefined();
    });
    it("should have a applyForCommand function", function () {
        expect(Bifrost.validation.validationService.applyForCommand).toBeDefined();
    });
}));