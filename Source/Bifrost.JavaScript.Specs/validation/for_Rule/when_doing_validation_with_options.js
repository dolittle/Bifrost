describe("when doing validation with options", function () {


    var rules;

    beforeEach(function () {
        rules = Bifrost.validation.ruleHandlers;
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                    Bifrost.validation.ruleHandlers.knownRule.optionsPassed = options;
                }
            }
        };
    });


    afterEach(function () {
        Bifrost.validation.ruleHandlers = rules;
    });
    it("should forward options to rule handler", function () {
        var options = { something: "hello world" };
        var rule = Bifrost.validation.Rule.create("knownRule", options);
        rule.validate("something");
        expect(Bifrost.validation.ruleHandlers.knownRule.optionsPassed.something).toBe(options.something);
    });
});