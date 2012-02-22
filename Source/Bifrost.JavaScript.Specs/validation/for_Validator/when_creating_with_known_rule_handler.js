describe("when creating with known rule handler", function () {
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
            }
        }
    });

    it("should create a validator", function () {
        var validator = Bifrost.validation.Validator.create({
            knownRule: {}
        });
        expect(validator).not.toBeUndefined();
    });

    it("should create a rule", function () {
        var validator = Bifrost.validation.Validator.create({
            knownRule: {}
        });
        expect(Bifrost.validation.Rule.createCalled).toBeTruthy();
    });

    it("should pass the options to the rule", function () {
        var options = { something: "hello world" };
        var validator = Bifrost.validation.Validator.create({
            knownRule: options
        });
        expect(Bifrost.validation.Rule.optionsPassed).toEqual(options);
    });

    it("should pass the rule name to the rule", function () {
        var options = { something: "hello world" };
        var validator = Bifrost.validation.Validator.create({
            knownRule: options
        });
        expect(Bifrost.validation.Rule.ruleNamePassed).toEqual("knownRule");
    });
});