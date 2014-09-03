Bifrost.namespace("Bifrost.specifications", {
    Or: Bifrost.specifications.Specification.extend(function (leftHandSide, rightHandSide) {
        /// <summary>Represents the "or" composite rule based on the specification pattern</summary>

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