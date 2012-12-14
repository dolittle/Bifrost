Bifrost.namespace("Bifrost.validation");
Bifrost.validation.validationService = (function () {
    function extendProperties(target, validators) {
        for (var property in target) {
            if ("extend" in target[property] && typeof target[property].extend === "function") {
                target[property].extend({ validation: {} });
                validators.push(target[property].validator);
            } else if (typeof target[property] === "object") {
                extendProperties(target[property], validators);
            }
        }
    }

    return {
        applyRulesToProperties: function (properties, rules) {
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

                if (member.validator !== undefined) {
                    member.validator.setOptions(rules[rule]);
                }
            }
        },
        applyForCommand: function (command) {
            command.validators = [];
            extendProperties(command.parameters, command.validators);
            $.getJSON("/Validation/GetForCommand?name=" + command.name, function (e) {
                if(e !== null) {
                    Bifrost.validation.validationService.applyRulesToProperties(command.parameters, e.properties);
                }
            });
        }
    }
})();