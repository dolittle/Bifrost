describe("when validating with multiple validators and first rule fails", function () {
    var validator;
    var options = {
        firstRule: {
            message: "First rule"
        },
        secondRule: {
            message: "Second rule"
        }
    };
    var firstRule = {
        _name: "firstRule",
        create: function (dependencies) {
            return {
                message: dependencies.options.message,
                validate: function () {
                    return false;
                }
            }
        }
    };

    var secondRuleValidateFunction = sinon.stub();
    var secondRule = {
        _name: "secondRule",
        create: function (dependencies) {
            return {
                message: dependencies.options.message,
                validate: secondRuleValidateFunction
            }
        }
    };

    beforeEach(function () {
        Bifrost.validation.Rule = {
            getExtenders: function () {
                return [firstRule, secondRule];
            }
        };

        validator = Bifrost.validation.Validator.create(options);
        validator.validate("something");
    });

    it("should not run subsequent rules", function () {
        expect(secondRuleValidateFunction.called).toBe(false);
    });
});