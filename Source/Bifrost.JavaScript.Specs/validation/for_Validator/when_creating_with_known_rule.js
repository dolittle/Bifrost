describe("when creating with known rule", function () {
    var options = { something: "hello world" };
    var validator = null;
    var knownRule = null;

    beforeEach(function () {
        knownRule = {
            _name: "knownRule",
            create: sinon.mock().withArgs({ ruleName: "knownRule", options: options }).once()
        };
        Bifrost.validation.Rule = {
            getExtenders: function () {
                return [knownRule];
            }
        }

        validator = Bifrost.validation.Validator.create({
            knownRule: options
        });
    });

    it("should create a validator", function () {
        expect(validator).not.toBeUndefined();
    });

    it("should create a rule with correct name and options passed along", function () {
        expect(knownRule.create.called).toBe(true);
    });
});