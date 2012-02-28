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
    beforeEach(function () {
        Bifrost.validation.Rule = {
            create: function (ruleName, options) {
                return {
                    message: options.message,
                    validate: function (value, options) {
                        if (ruleName == "firstRule") {
                            return false;
                        }
                        return true;
                    }
                }
            }
        }

        validator = Bifrost.validation.Validator.create(options);
    });

    it("should not run subsequent rules", function () {
        validator.validate("something");
        expect(validator.message()).toBe(options.firstRule.message);
    });
});