Bifrost.namespace("Bifrost.commands", {
    commandValidationService: Bifrost.Singleton(function () {
        var self = this;

        function shouldSkipProperty(target, property) {
            if (target instanceof Bifrost.views.Region) return true;
            if (target instanceof HTMLElement) return true;
            if (!target.hasOwnProperty(property)) return true;
            if (ko.isObservable(target[property])) return false;
            if (typeof target[property] === "function") return true;
            if (property == "_type") return true;
            if (property == "_namespace") return true;
            if ((target[property] == null) ) return true;
            if ((typeof target[property].prototype !== "undefined") &&
                (target[property].prototype !== null) &&
                (target[property] instanceof Bifrost.Type)) {
                return true;
            }

            return false;
        }

        function extendProperties(target, validators) {
            for (var property in target) {
                if (shouldSkipProperty(target, property)) continue;
                if (typeof target[property].validator != "undefined") continue;

                if (ko.isObservable(target[property])) {
                    target[property].extend({ validation: {} });
                    target[property].validator.propertyName = property;
                } else if (typeof target[property] === "object") {
                    extendProperties(target[property], validators);
                }
            }
        }

        function validatePropertiesFor(target, result, silent) {
            for (var property in target) {
                if (shouldSkipProperty(target, property)) continue;

                if (typeof target[property].validator !== "undefined") {
                    if (silent === true) {
                        target[property].validator.validateSilently(target[property]());
                    } else {
                        target[property].validator.validate(target[property]());
                    }

                    if (target[property].validator.isValid() == false) {
                        result.valid = false;
                    }
                } else if (typeof target[property] === "object") {
                    validatePropertiesFor(target[property], result, silent);
                }
            }
        }


        function applyValidationMessageToMembers(command, members, message) {
            for (var memberIndex = 0; memberIndex < members.length; memberIndex++) {
                var path = members[memberIndex].split(".");
                var property = null;
                var target = command;
                path.forEach(function (member) {
                    property = member.toCamelCase();
                    if (property in target) {
                        if (typeof target[property] === "object") {
                            target = target[property];
                        }
                    }
                });

                if (property != null && property.length) {
                    var member = target[property];

                    if (typeof member.validator !== "undefined") {
                        member.validator.isValid(false);
                        member.validator.message(message);
                    }
                }

            }
        }

        this.applyValidationResultToProperties = function (command, validationResults) {

            for (var i = 0; i < validationResults.length; i++) {
                var validationResult = validationResults[i];
                var message = validationResult.errorMessage;
                var memberNames = validationResult.memberNames;
                if (memberNames.length > 0) {
                    applyValidationMessageToMembers(command, memberNames, message);
                }
            }
        };

        this.validate = function (command) {
            var result = { valid: true };
            validatePropertiesFor(command, result);
            return result;
        };
        
        this.validateSilently = function (command) {
            var result = { valid: true };
            validatePropertiesFor(command, result, true);
            return result;
        };

        this.extendPropertiesWithoutValidation = function (command) {
            extendProperties(command);
        };


        function collectValidators(source, validators) {
            for (var property in source) {
                var value = source[property];

                if (shouldSkipProperty(source, property)) continue;

                if (ko.isObservable(value) && typeof value.validator != "undefined") {
                    validators.push(value.validator);
                } else if (Bifrost.isObject(value)) {
                    collectValidators(value, validators);
                }
            }
        }

        this.getValidatorsFor = function (command) {
            var validators = [];
            collectValidators(command, validators);
            return validators;
        };
    })
});