describe("when validating", function () {
    var rules;
    beforeEach(function () {
        rules = Bifrost.validation.ruleHandlers;
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                    Bifrost.validation.ruleHandlers.knownRule.validateCalled = true;
                    Bifrost.validation.ruleHandlers.knownRule.valueCalledWith = value;
                    return true;
                }
            }
        };
    });


    afterEach(function () {
        Bifrost.validation.ruleHandlers = rules;
    });

    it("should forward call to rule handler", function () {
        var rule = Bifrost.validation.Rule.create("knownRule", {});
        rule.validate("something");
        expect(Bifrost.validation.ruleHandlers.knownRule.validateCalled).toBeTruthy();
    });

    it("should forward value to rule handler", function () {
        var value = "something";
        var rule = Bifrost.validation.Rule.create("knownRule", {});
        rule.validate(value);
        expect(Bifrost.validation.ruleHandlers.knownRule.valueCalledWith).toBe(value);
    });

    it("should return the return value from the handler", function () {
        var rule = Bifrost.validation.Rule.create("knownRule", {});
        var result = rule.validate("something");
        expect(result).toBeTruthy();
    });
});