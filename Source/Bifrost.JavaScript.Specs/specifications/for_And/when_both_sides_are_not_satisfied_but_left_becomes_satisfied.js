describe("when both sides are not satisfied but left becomes satisfied", function () {
    
    var leftHandSideEvaluator = ko.observable(false);
    var leftHandSide = Bifrost.specifications.Specification.create()
    leftHandSide.evaluator = leftHandSideEvaluator;

    var rightHandSideEvaluator = ko.observable(false);
    var rightHandSide = Bifrost.specifications.Specification.create();
    rightHandSide.evaluator = rightHandSideEvaluator;

    var instance = { something: 42 };
    var rule = Bifrost.specifications.And.create({
        leftHandSide: leftHandSide,
        rightHandSide: rightHandSide
    });
    rule.evaluate(instance);

    var result = false;
    rule.isSatisfied.subscribe(function (newValue) {
        result = newValue;
    });
    result = false;

    leftHandSideEvaluator(true);

    it("should not be considered satisfied", function () {
        expect(result).toBe(false);
    });
});