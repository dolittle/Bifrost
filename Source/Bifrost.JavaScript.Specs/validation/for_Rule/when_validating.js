describe("when validating", function () {
    var value = "something";
    var rule = null;
    var result = null;

    beforeEach(function () {
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: sinon.mock().withArgs(value).once().returns(true)
            }
        };

        rule = Bifrost.validation.Rule.create({ ruleName: "knownRule", options: {} });
        result = rule.validate(value);
    });

    it("should forward call to rule handler with value", function () {
        expect(Bifrost.validation.ruleHandlers.knownRule.validate.called).toBe(true);
    });

    it("should return the return value from the handler", function () {
        expect(result).toBeTruthy();
    });
});