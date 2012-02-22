describe("when creating with message", function () {
    beforeEach(function () {
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                }
            }
        };
    });

    it("should set message in rule", function () {
        var errorMessage = "This is just wrong";
        var rule = Bifrost.validation.Rule.create("knownRule", { message: errorMessage });
        expect(rule.message).toBe(errorMessage);
    });

});