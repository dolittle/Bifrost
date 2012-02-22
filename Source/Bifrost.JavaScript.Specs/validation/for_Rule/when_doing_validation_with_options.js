describe("when doing validation with options", function () {
    beforeEach(function () {
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                    Bifrost.validation.ruleHandlers.knownRule.optionsPassed = options;
                }
            }
        };
    });

    it("should forward options to rule handler", function () {
        var options = { something: "hello world" };
        var rule = Bifrost.validation.Rule.create("knownRule", options);
        rule.validate("something");
        expect(Bifrost.validation.ruleHandlers.knownRule.optionsPassed.something).toBe(options.something);
    });
});