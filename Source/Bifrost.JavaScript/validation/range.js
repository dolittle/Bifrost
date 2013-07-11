Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.range = {
    throwIfOptionsInvalid: function (options) {
        if (this.notSet(options)) {
            throw new Bifrost.validation.OptionsNotDefined();
        }
        if (this.notSet(options.max)) {
            throw new Bifrost.validation.MaxNotSpecified();
        }
        if (this.notSet(options.min)) {
            throw new Bifrost.validation.MinNotSpecified();
        }
        this.throwIfValueIsNotANumber(options.min, "min")
        this.throwIfValueIsNotANumber(options.max, "max")
    },

    throwIfValueIsNotANumber: function (value, param) {
        if (!Bifrost.isNumber(value)) {
            throw new Bifrost.validation.NotANumber(param + " value " + value + " is not a number");
        }
    },

    validate: function (value, options) {
        this.throwIfOptionsInvalid(options);
        if (this.notSet(value)) {
            return false;
        }
        this.throwIfValueIsNotANumber(value, "value");
        return options.min <= value && value <= options.max;
    },

    notSet: function (value) {
        return Bifrost.isUndefined(value) || Bifrost.isNull(value);
    },
};

