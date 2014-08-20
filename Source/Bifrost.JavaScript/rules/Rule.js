Bifrost.namespace("Bifrost.rules", {
    Rule: Bifrost.Type.extend(function () {
        /// <summary>Represents a rule based on the specification pattern</summary>
        var self = this;
        var currentInstance = ko.observable();

        /// <field name="evaluator">
        /// Holds the evaluator to be used to evaluate wether or not the rule is satisfied
        /// </field>
        /// <remarks>
        /// The evaluator can either be a function that gets called with the instance
        /// or an observable. The observable not being a regular function will obviously
        /// not have the instance passed 
        /// </remarks>
        this.evaluator = null;

        /// <field name="isSatisfied">Observable that holds the result of any evaluations being done</field>
        /// <remarks>
        /// Due to its nature of being an observable, it will re-evaluate if the evaluator
        /// is an observable and its state changes.
        /// </remarks>
        this.isSatisfied = ko.computed(function () {
            
            if (ko.isObservable(self.evaluator)) {
                return self.evaluator();
            }
            var instance = currentInstance();

            if (!Bifrost.isNullOrUndefined(instance)) {
                return self.evaluator(instance);
            }
            return false;
        });

        this.evaluate = function (instance) {
            /// <summary>Evaluates the rule</summary>
            /// <param name="instance">Object instance used during evaluation</param>
            /// <returns>True if satisfied, false if not</returns>
            currentInstance(instance);

            return self.isSatisfied();
        };

        this.and = function (rule) {
            /// <summary>Takes this rule and "ands" it together with another rule</summary>
            /// <param name="rule">
            /// This can either be the instance of another specific rule, 
            /// or an evaluator that can be used directly by the default rule implementation
            /// </param>
            /// <returns>A new composed rule</returns>

            if (Bifrost.isFunction(rule)) {
                var oldRule = rule;
                rule = Bifrost.rules.Rule.create();
                rule.evaluator = oldRule;
            }

            var and = Bifrost.rules.And.create(this, rule);
            return and;
        };

        this.or = function (rule) {
            /// <summary>Takes this rule and "ors" it together with another rule</summary>
            /// <param name="rule">
            /// This can either be the instance of another specific rule, 
            /// or an evaluator that can be used directly by the default rule implementation
            /// </param>
            /// <returns>A new composed rule</returns>

            if (Bifrost.isFunction(rule)) {
                var oldRule = rule;
                rule = Bifrost.rules.Rule.create();
                rule.evaluator = oldRule;
            }

            var or = Bifrost.Rules.Or.create(this, rule);
            return or;
        };
    })
});
Bifrost.rules.Rule.when = function (evaluator) {
    /// <summary>Starts a rule chain</summary>
    /// <param name="evaluator">
    /// The evaluator can either be a function that gets called with the instance
    /// or an observable. The observable not being a regular function will obviously
    /// not have the instance passed 
    /// </param>
    /// <returns>A new composed rule</returns>
    var rule = Bifrost.rules.Rule.create();
    rule.evaluator = evaluator;
    return rule;
};