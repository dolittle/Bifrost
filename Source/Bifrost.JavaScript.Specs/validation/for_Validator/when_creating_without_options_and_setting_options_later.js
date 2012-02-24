describe("when creating without options and setting options later", function () {
    beforeEach(function () {
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                }
            }
        };
        Bifrost.validation.Rule = {
            create: function (ruleName, options) {
                Bifrost.validation.Rule.ruleNamePassed = ruleName;
                Bifrost.validation.Rule.createCalled = true;
                Bifrost.validation.Rule.optionsPassed = options;

                return {
                    validate: function () {
                        Bifrost.validation.Rule.validateCalled = true;
                    }
                }
            }
        }
    });

    it("should create a rule", function () {
        var rules = { knownRule: {} };
        var validator = Bifrost.validation.Validator.create();
        validator.setOptions(rules);
        expect(Bifrost.validation.Rule.createCalled).toBeTruthy();
    });

    it("should pass the options to the rule", function () {
        var options = { something: "hello world" };
        var rules = { knownRule: options };
        var validator = Bifrost.validation.Validator.create();
        validator.setOptions(rules);
        expect(Bifrost.validation.Rule.optionsPassed).toEqual(options);
    });

    it("should run against the rule when validating", function () {
        var options = { something: "hello world" };
        var rules = { knownRule: options };
        var validator = Bifrost.validation.Validator.create();
        validator.setOptions(rules);
        validator.validate("something");
        expect(Bifrost.validation.Rule.validateCalled).toBeTruthy();
    });
});