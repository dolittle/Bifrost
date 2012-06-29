Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.greaterThan = {
    throwIfOptionsUndefined: function (options) {
        if (!options || typeof options === "undefined") {
            throw new Bifrost.validation.OptionsNotDefined();
        }
    },
    throwIfValueUndefined: function (options) {
        if (typeof options.value === "undefined") {
            throw new Bifrost.validation.ValueNotSpecified();
        }
    },
    throwIfNotANumber: function (value) {
        if (!Bifrost.isNumber(value)) {
            throw new Bifrost.validation.NotANumber("Value " + value + " is not a number");
        }
    },

    validate: function (value, options) {
        this.throwIfNotANumber(value);
        this.throwIfOptionsUndefined(options);
        this.throwIfValueUndefined(options);
        if (typeof value === "undefined") {
            return false;
        }
        return parseFloat(value) > parseFloat(options.value);
    }
};
