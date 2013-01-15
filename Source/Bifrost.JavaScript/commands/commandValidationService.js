Bifrost.namespace("Bifrost.commands", {
    commandValidationService: Bifrost.Singleton(function (validationService) {
        var self = this;
        this.validationService = validationService;

        function extendProperties(target) {
            for (var property in target) {
                if (target[property].hasOwnProperty("extend") && typeof target[property].extend === "function") {
                    target[property].extend({ validation: {} });
                } else if (typeof target[property] === "object") {
                    extendProperties(target[property]);
                }
            }
        }

        function validatePropertiesFor(target) {
            for (var property in target) {
                if (typeof target[property].validator !== "undefined") {
                    target[property].validator.validate();
                } else if (typeof target[property] === "object") {
                    validatePropertiesFor(target[property]);
                }
            }
        }


        function applyValidationMessageToMembers(command, members, message) {
            for (var memberIndex = 0; memberIndex < members.length; memberIndex++) {
                var path = members[memberIndex].split(".");
                var property = null;
                var target = command;
                $.each(path, function (pathIndex, member) {
                    property = member.toCamelCase();
                    if (property in target) {
                        if (typeof target[property] === "object") {
                            target = target[property];
                        }
                    }
                });

                if (property != null) {
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
            validatePropertiesFor(command);
        };

        this.applyRulesTo = function (command) {
            extendProperties(command);
            self.validationService.getForCommand(command.name).continueWith(function (rules) {
                for (var rule in rules) {
                    var path = rule.split(".");
                    var member = command;
                    for (var i in path) {

                        var step = path[i];
                        if (step in member) {
                            member = member[step];
                        } else {
                            throw "Error applying validation rules: " + step + " is not a member of " + member + " (" + rule + ")";
                        }
                    }

                    if (member.validator !== undefined) {

                        member.validator.setOptions(rules[rule]);
                    }
                }
            });
        };
    })
});