describe("when doing validation with options", function () {
    var options = { something: "hello world" };
    var rule = null;

    beforeEach(function () {
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                    Bifrost.validation.ruleHandlers.knownRule.optionsPassed = options;
                }
            }
        };

        rule = Bifrost.validation.Rule.create({ ruleName: "knownRule", options: options });
        rule.validate("something");
    });

    it("should forward options to rule handler", function () {
        expect(Bifrost.validation.ruleHandlers.knownRule.optionsPassed.something).toBe(options.something);
    });
});