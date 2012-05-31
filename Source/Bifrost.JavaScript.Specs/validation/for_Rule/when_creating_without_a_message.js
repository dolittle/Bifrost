describe("when creating without message", function () {

    var rules;

    beforeEach(function () {
        rules = Bifrost.validation.ruleHandlers;
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                }
            }
        };
    });

    afterEach(function () {
        Bifrost.validation.ruleHandlers = rules;
    });

    it("should set message in rule", function () {
        var rule = Bifrost.validation.Rule.create("knownRule", {});
        expect(rule.message).toBe("");
    });

});