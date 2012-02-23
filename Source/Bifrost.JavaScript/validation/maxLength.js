Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.maxLength = {
    validate: function (value, options) {
        if (typeof options === "undefined" || typeof options.max === "undefined") {
            throw {
                message: "max is not specified for the maxLength validator"
            }
        }

        if (typeof value === "undefined") {
            return false;
        }

        return value.length <= options.max;
    }
};
