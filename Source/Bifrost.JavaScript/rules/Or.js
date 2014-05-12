Bifrost.namespace("Bifrost.rules", {
    Or: Bifrost.rules.Rule.extend(function (leftHandSide, rightHandSide) {
        this.isSatisfied = ko.computed(function () {
            return leftHandSide.isSatisfied() ||
                rightHandSide.isSatisfied();
        });

        this.evaluate = function (instance) {
            leftHandSide.evaluate(instance);
            rightHandSide.evaluate(instance);
        };
    })
});