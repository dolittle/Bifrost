describe("when creating without message", function () {

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
    

    it("should set message in rule", function () {
        expect(rule.message).toBe("");
    });

});