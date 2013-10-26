describe("when creating with known rule handler", function () {
    var options = { something: "hello world" };
    var validator = null;

    beforeEach(function () {
        Bifrost.validation.ruleHandlers = {
            knownRule: {
                validate: function (value, options) {
                }
            }
        };
        Bifrost.validation.Rule = {
            create: sinon.mock().withArgs({ ruleName: "knownRule", options: options }).once()
        }
        
        validator = Bifrost.validation.Validator.create({
            knownRule: options
        });
    });

    it("should create a validator", function () {
        expect(validator).not.toBeUndefined();
    });

    it("should create a rule with correct name and options passed along", function () {
        expect(Bifrost.validation.Rule.create.called).toBe(true);
    });
});