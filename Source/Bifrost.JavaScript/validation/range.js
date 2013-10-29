Bifrost.namespace("Bifrost.validation", {
    range: Bifrost.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return Bifrost.isUndefined(value) || Bifrost.isNull(value);
        }

        function throwIfValueIsNotANumber(value, param) {
            if (!Bifrost.isNumber(value)) {
                throw new Bifrost.validation.NotANumber(param + " value " + value + " is not a number");
            }
        }


        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new Bifrost.validation.OptionsNotDefined();
            }
            if (notSet(options.max)) {
                throw new Bifrost.validation.MaxNotSpecified();
            }
            if (notSet(options.min)) {
                throw new Bifrost.validation.MinNotSpecified();
            }
            throwIfValueIsNotANumber(options.min, "min")
            throwIfValueIsNotANumber(options.max, "max")
        }


        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIfValueIsNotANumber(value, "value");
            return self.options.min <= value && value <= self.options.max;
        };

    })
});
