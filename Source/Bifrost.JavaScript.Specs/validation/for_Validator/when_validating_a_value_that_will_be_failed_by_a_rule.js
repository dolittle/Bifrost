describe("when validating a value that will be failed by a rule", function () {
    var validator;
    var options = {
        someRule: {
            message: "The message"
        }
    };
    var someRule = null;

    beforeEach(function () {
        someRule = {
            _name: "someRule",
            create: function(dependencies) {
                return {
                    message: dependencies.options.message,
                    validate: function (value, options) {
                        return false;
                    }
                };
            }
        };
        Bifrost.validation.Rule = {
            getExtenders: function () {
                return [someRule];
            }
        };

        validator = Bifrost.validation.Validator.create(options);
        validator.validate("something");
    });

    it("should set isValid to false", function () {
        expect(validator.isValid()).toBe(false);
    });

    it("should set message", function () {
        expect(validator.message()).toBe(options.someRule.message);
    });
});