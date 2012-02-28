describe("when validating a value that will pass by a rule", function () {
    var validator;
    var options = {
        someRule: {
            message: "The message"
        }
    };
    beforeEach(function () {
        Bifrost.validation.Rule = {
            create: function (ruleName, options) {
                return {
                    message: options.message,
                    validate: function (value, options) {
                        return true;
                    }
                }
            }
        }

        validator = Bifrost.validation.Validator.create(options);
    });

    it("should set isValid to true", function () {
        validator.validate("something");
        expect(validator.isValid()).toBeTruthy();
    });

    it("should set an empty message", function () {
        validator.validate("something");
        expect(validator.message()).toBe("");
    });
});