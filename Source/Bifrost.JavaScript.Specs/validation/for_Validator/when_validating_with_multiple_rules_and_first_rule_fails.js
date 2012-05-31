describe("when validating with multiple validators and first rule fails", function () {
    var validator,
        ruleSpy,
        options = {
        firstRule: {
            message: "First rule"
        },
        secondRule: {
            message: "Second rule"
      
        }
    };

    beforeEach(function () {
        ruleSpy = sinon.stub(Bifrost.validation.Rule, "create", function (ruleName, options) {
            return {
                message: options.message,
                validate: function (value, options) {
                    if (ruleName == "firstRule") {
                        return false;
                    }
                    return true;
                }
            }
        });


        validator = Bifrost.validation.Validator.create(options);
    });

    afterEach(function () {
        ruleSpy.restore();
    })

    it("should not run subsequent rules", function () {
        validator.validate("something");
        expect(validator.message()).toBe(options.firstRule.message);
    });
});