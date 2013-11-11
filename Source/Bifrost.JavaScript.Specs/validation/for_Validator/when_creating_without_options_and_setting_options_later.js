describe("when creating without options and setting options later", function () {
    var options = { something: "hello world" };
    var rules = { knownRule: options };
    var validator = null;
    var knownRule = null;

    beforeEach(function () {
        knownRule = {
            _name: "knownRule",
            create: sinon.mock().withArgs({ options: options }).once()
        };

        Bifrost.validation.Rule = {
            getExtenders: function () {
                return [knownRule];
            }
        }

        validator = Bifrost.validation.Validator.create();
        validator.setOptions(rules);
    });

    it("should create a rule with correct name and options", function () {
        expect(knownRule.create.called).toBe(true);
    });
});