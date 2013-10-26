describe("when creating with a known rule and options", function () {
    var rule = null;
    var options = { something: "hello world" };

    beforeEach(function () {
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                }
            }
        };
        
        rule = Bifrost.validation.Rule.create({ ruleName: "knownRule", options: options });
    });

    it("should return a rule with the options set", function () {
        expect(rule.options).not.toBeUndefined();
    });

    it("should merge the content of the options", function () {
        expect(rule.options.something).toEqual(options.something);
    });
});