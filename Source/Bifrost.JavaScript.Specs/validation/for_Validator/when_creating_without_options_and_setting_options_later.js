describe("when creating without options and setting options later", function () {
    var options = { something: "hello world" };
    var rules = { knownRule: options };
    var validator = null;

    beforeEach(function () {
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                }
            }
        };
        Bifrost.validation.Rule = {
            create: sinon.mock().withArgs({ ruleName: "knownRule", options: options }).once()
        }

        validator = Bifrost.validation.Validator.create();
        validator.setOptions(rules);
    });

    it("should create a rule with correct name and options", function () {
        expect(Bifrost.validation.Rule.create.called).toBe(true);
    });
});