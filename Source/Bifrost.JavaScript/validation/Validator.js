Bifrost.namespace("Bifrost.validation");
Bifrost.validation.Validator = (function () {
    function Validator(options) {
        var self = this;
        this.isValid = ko.observable(true);
        this.message = ko.observable("");

        this.validate = function (value) {
            if (typeof value == "undefined" || value == "") {
                self.isValid(false);
                self.message("Yes we can");
            } else {
                self.isValid(true);
                self.message("");
            }
        }
    }

    return {
        create: function (options) {
            var validator = new Validator(options);
            return validator;
        },
        applyTo: function (item, options) {
            var validator = this.create(options);
            item.validator = validator;
        }
    }
})();
