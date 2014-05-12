Bifrost.namespace("Bifrost.rules", {
    And: Bifrost.rules.Rule.extend(function (leftHandSide, rightHandSide) {
        /// <summary>Represents the "and" composite rule based on the specification pattern</summary>

        this.isSatisfied = ko.computed(function () {
            return leftHandSide.isSatisfied() &&
                rightHandSide.isSatisfied();
        });

        this.evaluate = function (instance) {
            leftHandSide.evaluate(instance);
            rightHandSide.evaluate(instance);
        };
    })
});