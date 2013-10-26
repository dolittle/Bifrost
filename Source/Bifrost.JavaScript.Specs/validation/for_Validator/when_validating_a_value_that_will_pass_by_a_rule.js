describe("when validating a value that will pass by a rule", function () {
    var validator;
    var options = {
        someRule: {
            message: "The message"
        }
    };
    beforeEach(function () {
        someRule = {
            _name: "someRule",
            create: function (dependencies) {
                return {
                    message: dependencies.options.message,
                    validate: function (value, options) {
                        return true;
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

    it("should set isValid to true", function () {
        expect(validator.isValid()).toBe(true);
    });

    it("should set an empty message", function () {
        expect(validator.message()).toBe("");
    });
});