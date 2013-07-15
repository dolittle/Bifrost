Bifrost.namespace("Bifrost.validation.ruleHandlers");

Bifrost.validation.ruleHandlers.regex = {
    throwIfOptionsInvalid: function (options) {
        if (this.notSet(options)) {
            throw new Bifrost.validation.OptionsNotDefined();
        }
        if (this.notSet(options.expression)) {
            throw new Bifrost.validation.MissingExpression();
        }
    },

    validate: function (value, options) {
        this.throwIfOptionsInvalid(options);
        if (this.notSet(value)) {
            return false;
        }
        if (!Bifrost.isString(value)) {
            throw new Bifrost.validation.NotAString("Value " + value + " is not a string.");
        }
        return (value.match(options.expression) == null) ? false : true;
    },
    
    notSet: function(value) {
        return Bifrost.isUndefined(value) || Bifrost.isNull(value);
    }
};
