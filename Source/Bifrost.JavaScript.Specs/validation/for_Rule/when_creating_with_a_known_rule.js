describe("when creating with a known rule", function () {
    beforeEach(function () {
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                }
            }
        };
    });

    it("should return a rule with the reference to the rulehandler", function () {
        var rule = Bifrost.validation.Rule.create("knownRule", {});
        expect(rule.handler).toEqual(Bifrost.validation.ruleHandlers.knownRule);
    });

    it("should have options set", function () {
        var rule = Bifrost.validation.Rule.create("knownRule", {});
        expect(rule.options).not.toBeUndefined();
    });
});