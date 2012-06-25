Bifrost.namespace("Bifrost.validation");
Bifrost.validation.validationService = (function () {

    var rules = [];

    function getRuleState(name) {
        var rule = getRule(name);
        if (rule) {
            return rule.state;
        } else {
            return false;
        }
    }

    function getRule(name) {
        for (var i = 0; i < rules.length; i++) {
            if (rules[i].name == name) {
                return rules[i];
            }
        }
        return false;
    }

    function findRules(commandName, callback) {
        var ruleState = getRuleState(commandName);
        if (ruleState == "loaded") {
            callback(getRule(commandName).rule);
        } else if (ruleState == "loading") {
            getRule(commandName).callbacks.push(callback);
        } else {

            rules.push({
                name: commandName,
                state: "loading",
                rule: false,
                callbacks: [callback]
            });

            var methodParameters = {
                name: "\"" + commandName + "\""
            };
            $.ajax({
                type: "POST",
                url: "/Validation/GetForCommand",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(methodParameters),
                complete: function(d) {
                    var result = $.parseJSON(d.responseText);
                    if (!result || !result.properties) {
                        return;
                    }
                    var rule = getRule(commandName);
                    rule.state = "loaded";
                    rule.rule = result.properties;
                    for (var i = 0; i < rule.callbacks.length; i++) {
                        var callback = rule.callbacks[i];
                        callback(result.properties);
                    }
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
            rules.length = 0;
        },

        applyForCommand: function (command) {
            Bifrost.validation.validationService.recursivlyExtendProperties(command.parameters, command.validatorsList);

            findRules(command.name, function (rules) {
                Bifrost.validation.validationService.recursivlyApplyRules(command.parameters, rules);
            });

            /*var methodParameters = {
            name: "\"" + command.name + "\""
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
            Bifrost.validation.validationService.recursivlyApplyRules(command.parameters, result.properties);
            }
            });*/
        }
    };
})();