Bifrost.namespace("Bifrost.validation");
Bifrost.validation.OptionsNotDefined = function (message) {
    this.prototype = Error.prototype;
    this.name = "OptionsNotDefined";
    this.message = message || "option was undefined";
}

Bifrost.validation.NotANumber = function (message) {
    this.prototype = Error.prototype;
    this.name = "NotANumber";
    this.message = message || "value is not a number";
}

Bifrost.validation.ValueNotSpecified = function (message) {
    this.prototype = Error.prototype;
    this.name = "ValueNotSpecified";
    this.message = message || "value is not specified";
}

Bifrost.validation.MinNotSpecified = function (message) {
    this.prototype = Error.prototype;
    this.name = "MinNotSpecified";
    this.message = message || "min is not specified";
}

Bifrost.validation.MaxNotSpecified = function (message) {
    this.prototype = Error.prototype;
    this.name = "MaxNotSpecified";
    this.message = message || "max is not specified";
}

Bifrost.validation.MinLengthNotSpecified = function (message) {
    this.prototype = Error.prototype;
    this.name = "MinLengthNotSpecified";
    this.message = message || "min length is not specified";
}

Bifrost.validation.MaxLengthNotSpecified = function (message) {
    this.prototype = Error.prototype;
    this.name = "MaxLengthNotSpecified";
    this.message = message || "max length is not specified";
}

Bifrost.validation.MissingExpression = function (message) {
    this.prototype = Error.prototype;
    this.name = "MissingExpression";
    this.message = message || "expression is not specified";
}