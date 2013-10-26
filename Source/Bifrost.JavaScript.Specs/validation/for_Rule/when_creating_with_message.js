describe("when creating with message", function () {
    var errorMessage = "This is just wrong";
    var rule = null;
    beforeEach(function () {
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                }
            }
        };
        rule = Bifrost.validation.Rule.create({ ruleName: "knownRule", options: { message: errorMessage } });
    });

    it("should set message in rule", function () {
        expect(rule.message).toBe(errorMessage);
    });
});