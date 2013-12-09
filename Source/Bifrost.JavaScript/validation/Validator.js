Bifrost.namespace("Bifrost.validation");
Bifrost.validation.Validator = (function () {
    function Validator(options) {
        var self = this;
        this.isValid = ko.observable(true);
        this.message = ko.observable("");
        this.rules = [];
        this.isRequired = false;
        options = options || {};

        this.setOptions = function (options) {
            for (var property in options) {
                var ruleTypes = Bifrost.validation.Rule.getExtenders();
                ruleTypes.some(function (ruleType) {
                    if (ruleType._name === property) {
                        var rule = ruleType.create({ options: options[property] || {} });
                        self.rules.push(rule);
                    }

                    if (ruleType._name === "required") {
                        self.isRequired = true;
                    }
                });
            }
        };

        this.validate = function(value) {
            self.rules.some(function(rule) {
                if (!rule.validate(value)) {
                    self.isValid(false);
                    self.message(rule.message);
                    return true;
                } else {
                    self.isValid(true);
                    self.message("");
                }
            });
        };

        this.validateSilently = function (value) {
            self.rules.some(function (rule) {
                if (!rule.validate(value)) {
                    self.isValid(false);
                    return true;
                } else {
                    self.isValid(true);
                }
            });
        };


        this.setOptions(options);
    }

    return {
        create: function (options) {
            var validator = new Validator(options);
            return validator;
        },
        applyTo: function (itemOrItems, options) {
            var self = this;

            function applyToItem(item) {
                var validator = self.create(options);
                item.validator = validator;
            }

            if (itemOrItems instanceof Array) {
                itemOrItems.forEach(function (item) {

                    applyToItem(item);
                });
            } else {
                applyToItem(itemOrItems);
            }
        },
        applyToProperties: function (item, options) {
            var items = [];

            for (var property in item) {
                if (item.hasOwnProperty(property)) {
                    items.push(item[property]);
                }
            }
            this.applyTo(items, options);
        }
    }
})();
