Bifrost.namespace("Bifrost.validation.ruleHandlers");

Bifrost.validation.ruleHandlers.regex = {
    throwIfOptionsUndefined: function (options) {
        if (typeof options === "undefined") {
            throw new Bifrost.validation.OptionsNotDefined();
        }
    },

    throwIfExpressionMissing: function (options) {
        if (!options.expression) {
            throw new Bifrost.validation.MissingExpression();
        }
    },

    validate: function (value, options) {
        this.throwIfOptionsUndefined(options);
        this.throwIfExpressionMissing(options);

        return (value.match(options.expression) == null) ? false : true;
    }
};
