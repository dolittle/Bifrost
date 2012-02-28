describe("when validating without options", function () {
    beforeEach(function () {
        Bifrost.validation.Rule = {
            create: function (options) {
                Bifrost.validation.Rule.optionsPassed = options;
            }
        }
    });

    it("should pass empty options rule creation", function () {
        var validator = Bifrost.validation.Validator.create({ rule: null });
        expect(Bifrost.validation.Rule.optionsPassed).not.toBeUndefined();
        expect(Bifrost.validation.Rule.optionsPassed).not.toBeNull();
    });
});