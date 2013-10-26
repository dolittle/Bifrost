describe("when creating with a known rule", function () {
    var rule = null;

    beforeEach(function () {
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                }
            }
        };

        rule = Bifrost.validation.Rule.create({ ruleName: "knownRule", options: {} });
    });

    it("should return a rule with the reference to the rulehandler", function () {
        expect(rule.handler).toEqual(Bifrost.validation.ruleHandlers.knownRule);
    });

    it("should have options set", function () {
        expect(rule.options).not.toBeUndefined();
    });
});