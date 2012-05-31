describe("when validating without options", function () {
    var ruleSpy;

    beforeEach(function () {
        ruleSpy = sinon.stub(Bifrost.validation.Rule, "create", function(options) {
            Bifrost.validation.Rule.optionsPassed = options;
        });
        
    });



    afterEach(function () {
        ruleSpy.restore();
    })

    it("should pass empty options rule creation", function () {
        var validator = Bifrost.validation.Validator.create({ rule: null });
        expect(Bifrost.validation.Rule.optionsPassed).not.toBeUndefined();
        expect(Bifrost.validation.Rule.optionsPassed).not.toBeNull();
    });
});