Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.minLength = {
    throwIfOptionsInvalid: function (options) {
        if (this.notSet(options)) {
            throw new Bifrost.validation.OptionsNotDefined();
        }
        if (this.notSet(options.length)) {
            throw new Bifrost.validation.MinNotSpecified();
        }
        this.throwIfValueIsNotANumber(options.length)
    },

    throwIfValueIsNotANumber: function (value) {
        if (!Bifrost.isNumber(value)) {
            throw new Bifrost.validation.NotANumber("Value " + value + " is not a number");
        }
    },
    
    throwIfValueIsNotAString: function (string) {
        if (!Bifrost.isString(string)) {
            throw new Bifrost.validation.NotAString("Value " + string + " is not a string");
        }
    },

    validate: function (value, options) {
        this.throwIfOptionsInvalid(options);
        if (this.notSet(value)) {
            return false;
        }
        this.throwIfValueIsNotAString(value);
        return value.length >= options.length;
    },
    
    notSet: function(value) {
        return Bifrost.isUndefined(value) || Bifrost.isNull(value);
    }, 
};
};
