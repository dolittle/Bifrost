Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.required = {
    validate: function (value, options) {
        return !(Bifrost.isUndefined(value) || Bifrost.isNull(value) || value == "");
    }
};
