describe("when creating without message", function () {
    beforeEach(function () {
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                }
            }
        };
    });

    it("should set message in rule", function () {
        var rule = Bifrost.validation.Rule.create("knownRule", {});
        expect(rule.message).toBe("");
    });

});