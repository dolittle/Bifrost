Bifrost.namespace("Bifrost.validation");
Bifrost.validation.validationService = (function () {

    var inputValidators = [];
    
    function getInputValidator(name) {
        for (var i = 0; i < inputValidators.length; i++) {
            if (inputValidators[i].name == name) {
                return inputValidators[i];
            }
        }
        return false;
    }

    function loadCommandRules(commandName, callback) {
        var methodParameters = {
            name: "\"" + commandName + "\""
        };
        $.ajax({
            type: "POST",
            url: "/Validation/GetForCommand",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(methodParameters),
            complete: function (d) {
                var result = $.parseJSON(d.responseText);
                if (!result || !result.properties) {
                    return;
                }
                callback(result.properties);
            }
        });
    }

    function getCommandRules(commandName, callback) {
        var inputValidator = getInputValidator(commandName);
        var ruleState = inputValidator ? inputValidator.state : false;
        if (ruleState == "loaded") {
            callback(inputValidator.rule);
        } else if (ruleState == "loading") {
            inputValidator.callbacks.push(callback);
        } else {

            inputValidator = {
                name: commandName,
                state: "loading",
                rule: false,
                callbacks: [callback]
            };

            inputValidators.push(inputValidator);

            loadCommandRules(commandName, function (properties) {
                inputValidator.state = "loaded";
                inputValidator.rule = properties;
                for (var i = 0; i < inputValidator.callbacks.length; i++) {
                    var ruleCallback = inputValidator.callbacks[i];
                    ruleCallback(properties);
                }
            });
        }
    }

    return {
        recursivlyExtendProperties: function (properties, validatorsList) {
            for (var key in properties) {
                var property = properties[key];
                if (ko.isObservable(property)) {
                    property.extend({ validation: {} });
                    validatorsList.push(property);
                }
                property = ko.utils.unwrapObservable(property);
                if (typeof property === "object") {
                    Bifrost.validation.validationService.recursivlyExtendProperties(property, validatorsList);
                }
            }
        },

        recursivlyApplyRules: function (properties, rules) {
            for (var rule in rules) {
                var path = rule.split(".");
                var memberName = "parameters";
                var member = properties;
                for (var i = 0; i < path.length; i++) {
                    var step = path[i];
                    member = ko.utils.unwrapObservable(member);
                    if (typeof member === "object" && step in member) {
                        memberName += "." + step;
                        member = member[step];
                    } else {
                        throw new Error("Error applying validation rule: `" + rule + "`\n" +
                            step + " is not a member of " + memberName + ". \n" +
                            memberName + "=`" + (ko.isObservable(member) ? member() : member) + "`");
                    }
                }

                if (ko.isObservable(member) && "validator" in member) {
                    member.validator.setOptions(rules[rule]);
                } else {
                    throw new Error("Error applying validation rule: " + rule + "\n" +
                        "It is not an observable or is not extended with a validator. \n" +
                        memberName + "=`" + (ko.isObservable(member) ? member() : member) + "`");
                }
            }
        },

        resetCache: function () {
            inputValidators.length = 0;
        },

        applyForCommand: function (command) {
            Bifrost.validation.validationService.recursivlyExtendProperties(ko.utils.unwrapObservable(command.parameters), command.validatorsList);

            getCommandRules(command.name, function (rules) {
                Bifrost.validation.validationService.recursivlyApplyRules(command.parameters, rules);
            });
        }
    };
})();