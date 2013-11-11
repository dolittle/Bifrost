Bifrost.namespace("Bifrost.validation", {
    regex: Bifrost.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return Bifrost.isUndefined(value) || Bifrost.isNull(value);
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new Bifrost.validation.OptionsNotDefined();
            }
            if (notSet(options.expression)) {
                throw new Bifrost.validation.MissingExpression();
            }
            if (!Bifrost.isString(options.expression)) {
                throw new Bifrost.validation.NotAString("Expression " + options.expression+ " is not a string.");
            }
        }

        function throwIfValueIsNotString(value) {
            if (!Bifrost.isString(value)) {
                throw new Bifrost.validation.NotAString("Value " + value + " is not a string.");
            }
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIfValueIsNotString(value);
            return (value.match(self.options.expression) == null) ? false : true;
        };
    })
});


