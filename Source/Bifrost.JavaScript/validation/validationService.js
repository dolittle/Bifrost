Bifrost.namespace("Bifrost.validation");
Bifrost.validation.validationService = (function () {
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

        applyForCommand: function (command) {
            Bifrost.validation.validationService.recursivlyExtendProperties(ko.utils.unwrapObservable(command.parameters), command.validatorsList);

            var methodParameters = {
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
                    Bifrost.validation.validationService.recursivlyApplyRules(ko.utils.unwrapObservable(command.parameters), result.properties);
                }
            });
        }
    };
})();