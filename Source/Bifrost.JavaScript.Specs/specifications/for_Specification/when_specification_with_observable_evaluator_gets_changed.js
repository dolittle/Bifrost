describe("when specification with observable evaluator gets changed", function () {

    var evaluator = ko.observable(false);
    var rule = Bifrost.specifications.Specification.create();
    rule.evaluator = evaluator;
    var instance = { something: 42 };
    rule.evaluate(instance);

    var result = false;
    rule.isSatisfied.subscribe(function (newValue) {
        result = newValue;
    });
    result = false;

    evaluator(true);

    it("should reevaluate isSatisfied", function () {
        expect(result).toBe(true);
    });
});