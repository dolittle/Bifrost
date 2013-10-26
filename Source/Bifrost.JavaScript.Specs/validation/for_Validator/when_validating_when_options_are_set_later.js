describe("when validating when options are set later", function () {
    var options = { something: "hello world" };
    var rules = { knownRule: options };
    var validator = null;
    var validateStub = sinon.stub();

    beforeEach(function () {
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                }
            }
        };
        Bifrost.validation.Rule = {
            create: function (ruleName, options) {
                return {
                    validate: validateStub
                }
            }
        }

        validator = Bifrost.validation.Validator.create({ ruleName: "", options: {} });
        validator.setOptions(rules);
        validator.validate("something");
    });

    it("should run against the rule when validating", function () {
        expect(validateStub.called).toBe(true);
    });
});