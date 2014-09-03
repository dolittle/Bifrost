describe("when specification is evaluated against an instance", function () {

    var evaluator = sinon.stub().returns(true);
    var rule = Bifrost.specifications.Specification.create();
    rule.evaluator = evaluator;
    var instance = { something: 42 };
    rule.evaluate(instance);

    var isSatisfied = rule.isSatisfied();

    it("should call the evaluator with the instance", function () {
        expect(evaluator.calledWith(instance)).toBe(true);
    });

    it("should be satisfied", function () {
        expect(isSatisfied).toBe(true);
    });
});