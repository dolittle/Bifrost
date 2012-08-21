Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.minLength = {
    validate: function (value, options) {
        if (typeof options === "undefined" || typeof options.length === "undefined") {
            throw {
                message: "length is not specified for the minLength validator"
            }
        }

        if (typeof value === "undefined") {
            return false;

        }

        return value.length >= options.length;
    }
};
