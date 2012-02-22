if (typeof ko !== 'undefined') {
    ko.extenders.validation = function (target, options) {
        Bifrost.validation.Validator.applyTo(target, options);
        target.subscribe(target.validator.validate);
        target.validator.validate(target());
        return target;
    };
}
