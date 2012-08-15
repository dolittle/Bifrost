Bifrost.namespace("Bifrost.validation");
Bifrost.validation.validationService = (function () {
    function extendProperties(target) {
        for( var property in target ) {
            if ("extend" in target[property] && typeof target[property].extend === "function") {
                target[property].extend({ validation: {} });
            } else if( typeof target[property] === "object" ) {
                extendProperties(target[property]);
            }
        }
    }

    return {
        applyRulesToProperties: function(properties, rules) {
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

                if (member.validator !== undefined ) {
                    member.validator.setOptions(rules[rule]);
                    validatorsList.push(member);
                } 
            }
            return validatorsList;
        },
        applyForCommand: function (command) {
            extendProperties(command.parameters);
            $.getJSON("/Validation/GetForCommand?name=" + command.name, function (e) {
                command.validatorsList = Bifrost.validation.validationService.applyRulesToProperties(command.parameters, e.properties);
            });
        }
    }
})();