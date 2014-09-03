describe("when left hand is satisfied and right hand is also", function () {
    
    var leftHandSideEvaluator = sinon.stub().returns(true);
    var leftHandSide = Bifrost.specifications.Specification.create()
    leftHandSide.evaluator = leftHandSideEvaluator;

    var rightHandSideEvaluator = sinon.stub().returns(true);
    var rightHandSide = Bifrost.specifications.Specification.create();
    rightHandSide.evaluator = rightHandSideEvaluator;

    var instance = { something: 42 };
    var rule = Bifrost.specifications.Or.create({
        leftHandSide: leftHandSide,
        rightHandSide: rightHandSide
    });
    rule.evaluate(instance);

    var satisfied = rule.isSatisfied();

    it("should be considered satisfied", function () {
        expect(satisfied).toBe(true);
    });
});