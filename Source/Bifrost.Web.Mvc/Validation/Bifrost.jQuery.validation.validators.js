$.validator.addMethod("greaterthan", function (value, element, params) {
    return value > params;
});
$.validator.unobtrusive.adapters.add("greaterthan", ["valuetocompare"], function (options) {
    options.rules["greaterthan"] = options.params.valuetocompare;
    options.messages["greaterthan"] = options.message;
});

$.validator.addMethod("greaterthanorequal", function (value, element, params) {
    return value > params;
});
$.validator.unobtrusive.adapters.add("greaterthanorequal", ["valuetocompare"], function (options) {
    options.rules["greaterthanorequal"] = options.params.valuetocompare;
    options.messages["greaterthanorequal"] = options.message;
});

$.validator.addMethod("lessthan", function (value, element, params) {
    return value < params;
});
$.validator.unobtrusive.adapters.add("lessthan", ["valuetocompare"], function (options) {
    options.rules["lessthan"] = options.params.valuetocompare;
    options.messages["lessthan"] = options.message;
});

$.validator.addMethod("lessthanorequal", function (value, element, params) {
    return value < params;
});
$.validator.unobtrusive.adapters.add("lessthanorequal", ["valuetocompare"], function (options) {
    options.rules["lessthanorequal"] = options.params.valuetocompare;
    options.messages["lessthanorequal"] = options.message;
});
