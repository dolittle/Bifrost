describe("when validating when options are set later", function () {
    var options = { something: "hello world" };
    var rules = { knownRule: options };
    var validator = null;
    var validateStub = sinon.stub();

    beforeEach(function () {
        var knownRule = {
            _name: "knownRule",
            create: function (dependencies) {
                return {
                    message: dependencies.options.message,
                    validate: validateStub
                }
            }
        };

        Bifrost.validation.Rule = {
            getExtenders: function () {
                return [knownRule];
            }
        };

        validator = Bifrost.validation.Validator.create({ ruleName: "", options: {} });
        validator.setOptions(rules);
        validator.validate("something");
    });

    it("should run against the rule when validating", function () {
        expect(validateStub.called).toBe(true);
    });
});