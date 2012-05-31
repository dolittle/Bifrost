describe("when creating with message", function () {

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
        var errorMessage = "This is just wrong";
        var rule = Bifrost.validation.Rule.create("knownRule", { message: errorMessage });
        expect(rule.message).toBe(errorMessage);
    });

});