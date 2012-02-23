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
