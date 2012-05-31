describe("when validating a value that will pass by a rule", function () {
    

    var validator,
        ruleSpy,
        options = {
            someRule: {
                message: "The message"
            }
        };
    beforeEach(function () {
        ruleSpy = sinon.stub(Bifrost.validation.Rule, "create", function (ruleName, options) {
            return {
                message: options.message,
                validate: function (value, options) {
                    return true;
                }
            }
        });


        validator = Bifrost.validation.Validator.create(options);
    });

    afterEach(function () {
        ruleSpy.restore();
    })

    it("should set isValid to true", function () {
        validator.validate("something");
        expect(validator.isValid()).toBeTruthy();
    });

    it("should set an empty message", function () {
        validator.validate("something");
        expect(validator.message()).toBe("");
    });
});