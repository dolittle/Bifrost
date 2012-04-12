Bifrost.namespace("Bifrost.validation");
Bifrost.validation.validationService = (function () {
    return {
        recursivlyExtendProperties: function (properties, rules) {
            var validatorsList = [];
            for (var rule in rules) {
                var path = rule.split(".");
                var member = properties;
                for (var i in path) {
                    var step = path[i];
                    if (step in member) {
                        member = member[step];
                    } else {
                        throw "Error applying validation rules: " + step + " is not a member of " + member + " (" + rule + ")";
                    }
                }

                if ("extend" in member && typeof member.extend === "function") {
                    member.extend({ validation: {} });
                    member.validator.setOptions(rules[rule]);
                    validatorsList.push(member);
                } else {
                    throw "Error applying validation rule: " + property + " is not an observable.";
                }
            }
            return validatorsList;
        },
        /*
        extendAllProperties: function (target) {
        for (var property in target) {
        if ("extend" in target[property] && typeof target[property].extend === "function") {
        target[property].extend({ validation: {} });
        }
        }
        },*/
        applyForCommand: function (command) {
            //Bifrost.validation.validationService.extendAllProperties(command.parameters);

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
                    command.validatorsList = Bifrost.validation.validationService.recursivlyExtendProperties(command.parameters, result.properties);
                    /*for (var property in result.properties) {
                    if (!command.parameters.hasOwnProperty(property)) {
                    command.parameters[property] = ko.observable().extend({ validation: {} });
                    }
                    command.parameters[property].validator.setOptions(result.properties[property]);
                    }*/
                }
            });
        }
    }
})();