Bifrost.namespace("Bifrost.rules", {
    Rule: Bifrost.Type.extend(function () {
        var self = this;
        var currentInstance = ko.observable();
        
        this.evaluator = null;

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
            currentInstance(instance);
        };

        this.and = function (rule) {
            if (Bifrost.isFunction(rule)) {
                var oldRule = rule;
                rule = Bifrost.rules.Rule.create();
                rule.evaluator = oldRule;
            }

            var and = Bifrost.rules.And.create(this, rule);
            return and;
        };

        this.or = function (rule) {
            var or = Bifrost.Rules.Or.create(this, rule);
            return or;
        };

    })
});