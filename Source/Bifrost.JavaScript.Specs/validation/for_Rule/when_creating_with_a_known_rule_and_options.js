describe("when creating with a known rule and options", function () {
    beforeEach(function () {
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                }
            }
        };
    });

    it("should return a rule with the options set", function () {
        var options = { something: "hello world" };
        var rule = Bifrost.validation.Rule.create("knownRule", options);
        expect(rule.options).not.toBeUndefined();
        expect(rule.options.something).toEqual(options.something);
    });
});