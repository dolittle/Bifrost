var Bifrost = Bifrost || {};
(function(global, undefined) {
    Bifrost.namespace = function (ns) {
        var parent = global;
        var parts = ns.split('.');
        $.each(parts, function (index, part) {
            if (!Object.prototype.hasOwnProperty.call(parent, part)) {
                parent[part] = {};
            }
            parent = parent[part];
        });
    };
})(window);
Bifrost.namespace("Bifrost");
Bifrost.extend = function extend(destination, source) {
    return $.extend(destination, source);
};

Bifrost.namespace("Bifrost");
Bifrost.Guid = (function () {
    function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }

    return {
        create: function () {
            return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4()); ;
        }
    }
})(); 
﻿Bifrost.namespace("Bifrost");
Bifrost.isNumber = function(number) {
    return !isNaN(parseFloat(number)) && isFinite(number);
}

Bifrost.namespace("Bifrost");
Bifrost.hashString = (function() {
	return {
		decode: function(a) {
		    if (a == "") return { };
			a = a.replace("/?","").split('&');

		    var b = { };
		    for (var i = 0; i < a.length; ++i) {
		        var p = a[i].split('=');
		        if (p.length != 2) continue;
		        b[p[0]] = decodeURIComponent(p[1].replace( /\+/g , " "));
		    }
		    return b;
		}
	}
})();

﻿Bifrost.namespace("Bifrost.validation");
Bifrost.validation.OptionsNotDefined = function (message) {
    this.prototype = Error.prototype;
    this.name = "OptionsNotDefined";
    this.message = message || "option was undefined";
}

Bifrost.validation.NotANumber = function (message) {
    this.prototype = Error.prototype;
    this.name = "NotANumber";
    this.message = message || "value is not a number";
}

Bifrost.validation.ValueNotSpecified = function (message) {
    this.prototype = Error.prototype;
    this.name = "ValueNotSpecified";
    this.message = message || "value is not specified";
}

Bifrost.validation.MinNotSpecified = function (message) {
    this.prototype = Error.prototype;
    this.name = "MinNotSpecified";
    this.message = message || "min is not specified";
}

Bifrost.validation.MaxNotSpecified = function (message) {
    this.prototype = Error.prototype;
    this.name = "MaxNotSpecified";
    this.message = message || "max is not specified";
}

Bifrost.validation.MinLengthNotSpecified = function (message) {
    this.prototype = Error.prototype;
    this.name = "MinLengthNotSpecified";
    this.message = message || "min length is not specified";
}

Bifrost.validation.MaxLengthNotSpecified = function (message) {
    this.prototype = Error.prototype;
    this.name = "MaxLengthNotSpecified";
    this.message = message || "max length is not specified";
}

Bifrost.validation.MissingExpression = function (message) {
    this.prototype = Error.prototype;
    this.name = "MissingExpression";
    this.message = message || "expression is not specified";
}
﻿Bifrost.namespace("Bifrost.validation");
Bifrost.validation.ruleHandlers = (function () {
    return {
    }
})();

﻿Bifrost.namespace("Bifrost.validation");
Bifrost.validation.Rule = (function () {
    function Rule(ruleName, options) {
        var self = this;
        this.handler = Bifrost.validation.ruleHandlers[ruleName];

        options = options || {};

        this.message = options.message || "";
        this.options = {};
        Bifrost.extend(this.options, options);

        this.validate = function (value) {
            return self.handler.validate(value, self.options);
        }
    }

    return {
        create: function (ruleName, options) {
            var rule = new Rule(ruleName, options);
            return rule;
        }
    };
})();
﻿Bifrost.namespace("Bifrost.validation");
Bifrost.validation.Validator = (function () {
    function Validator(options) {
        var self = this;
        this.isValid = ko.observable(true);
        this.message = ko.observable("");
        this.rules = [];
        options = options || {};

        this.setOptions = function (options) {
            for (var property in options) {
                this.rules.push(Bifrost.validation.Rule.create(property, options[property] || {}));
            }
        }

        this.validate = function (value) {
            $.each(self.rules, function (index, rule) {
                if (!rule.validate(value)) {
                    self.isValid(false);
                    self.message(rule.message);
                    return false;
                } else {
                    self.isValid(true);
                    self.message("");
                }
            });
        }

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
                $.each(itemOrItems, function (index, item) {
                    
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

﻿if (typeof ko !== 'undefined') {
    ko.bindingHandlers.validationMessageFor = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var value = valueAccessor();
            var validator = value.validator;
            ko.applyBindingsToNode(element, { hidden: validator.isValid, text: validator.message }, validator);
        }
    };
}
﻿if (typeof ko !== 'undefined') {
    ko.extenders.validation = function (target, options) {
        Bifrost.validation.Validator.applyTo(target, options);
        target.subscribe(function (newValue) {
            target.validator.validate(newValue);
        });

        // Todo : look into aggressive validation
        //target.validator.validate(target());
        return target;
    };
}

﻿Bifrost.namespace("Bifrost.validation");
Bifrost.validation.validationService = (function () {
    return {
        extendAllProperties: function (target) {
            for (var property in target) {
				if( typeof target[property] === "function" ) {
                	target[property].extend({ validation: {} });
				}
            }
        },
        applyForCommand: function (command) {
            Bifrost.validation.validationService.extendAllProperties(command.parameters);

            var methodParameters = {
                name: "\"" + command.name + "\""
            }
            $.ajax({
                type: "POST",
                url: "/Validation/GetForCommand",
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(methodParameters),
                complete: function (d) {
                    var result = $.parseJSON(d.responseText);
					if( !result || !result.properties ) {
						return;
					}
                    for (var property in result.properties) {
                        if (!command.parameters.hasOwnProperty(property)) {
                            command.parameters[property] = ko.observable().extend({ validation: {} });
                        }
                        command.parameters[property].validator.setOptions(result.properties[property]);
                    }
                }
            });
        }
    }
})();
﻿Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.required = {
    validate: function (value, options) {
        return !(typeof value == "undefined" || value == "");
    }
};

﻿Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.minLength = {
    validate: function (value, options) {
        if (typeof options === "undefined" || typeof options.length === "undefined") {
            throw {
                message: "length is not specified for the minLength validator"
            }
        }

        if (typeof value === "undefined") {
            return false;

        }

        return value.length >= options.length;
    }
};

﻿Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.maxLength = {
    validate: function (value, options) {
        if (typeof options === "undefined" || typeof options.length === "undefined") {
            throw {
                message: "length is not specified for the maxLength validator"
            }
        }

        if (typeof value === "undefined") {
            return false;
        }

        return value.length <= options.length;
    }
};

﻿Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.range = {
    isNumber: function (number) {
        return !isNaN(parseFloat(number)) && isFinite(number);
    },
    throwIfOptionsUndefined: function (options) {
        if (typeof options === "undefined") {
            throw new Bifrost.validation.OptionsNotDefined();
        }
    },
    throwIfMinUndefined: function (options) {
        if (typeof options.min === "undefined") {
            throw new Bifrost.validation.MinNotSpecified();
        }
    },
    throwIfMaxUndefined: function (options) {
        if (typeof options.max === "undefined") {
            throw new Bifrost.validation.MaxNotSpecified();
        }
    },
    throwIfNotANumber: function (value) {
        if (!Bifrost.isNumber(value)) {
            throw new Bifrost.validation.NotANumber("Value " + value + " is not a number");
        }
    },

    validate: function (value, options) {
        this.throwIfNotANumber(value);
        this.throwIfOptionsUndefined(options);
        this.throwIfMaxUndefined(options);
        this.throwIfMinUndefined(options);

        if (typeof value === "undefined") {
            return false;
        }

        return value <= options.max && value >= options.min;
    }
};

﻿Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.lessThan = {
    throwIfOptionsUndefined: function (options) {
        if (typeof options === "undefined") {
            throw new Bifrost.validation.OptionsNotDefined();
        }
    },
    throwIfValueUndefined: function (options) {
        if (typeof options.value === "undefined") {
            throw new Bifrost.validation.ValueNotSpecified();
        }
    },
    throwIfNotANumber: function (value) {
        if (!Bifrost.isNumber(value)) {
            throw new Bifrost.validation.NotANumber("Value " + value + " is not a number");
        }
    },

    validate: function (value, options) {
        this.throwIfNotANumber(value);
        this.throwIfOptionsUndefined(options);
        this.throwIfValueUndefined(options);
        if (typeof value === "undefined") {
            return false;
        }
        return parseFloat(value) < parseFloat(options.value);
    }
};

﻿Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.greaterThan = {
    throwIfOptionsUndefined: function (options) {
        if (!options || typeof options === "undefined") {
            throw new Bifrost.validation.OptionsNotDefined();
        }
    },
    throwIfValueUndefined: function (options) {
        if (typeof options.value === "undefined") {
            throw new Bifrost.validation.ValueNotSpecified();
        }
    },
    throwIfNotANumber: function (value) {
        if (!Bifrost.isNumber(value)) {
            throw new Bifrost.validation.NotANumber("Value " + value + " is not a number");
        }
    },

    validate: function (value, options) {
        this.throwIfNotANumber(value);
        this.throwIfOptionsUndefined(options);
        this.throwIfValueUndefined(options);
        if (typeof value === "undefined") {
            return false;
        }
        return parseFloat(value) > parseFloat(options.value);
    }
};

﻿Bifrost.namespace("Bifrost.validation.ruleHandlers");
Bifrost.validation.ruleHandlers.email = {
    regex : /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$/,

    validate: function (value, options) {
        return (value.match(this.regex) == null) ? false : true;
    }
};

﻿Bifrost.namespace("Bifrost.validation.ruleHandlers");

Bifrost.validation.ruleHandlers.regex = {
    throwIfOptionsUndefined: function (options) {
        if (typeof options === "undefined") {
            throw new Bifrost.validation.OptionsNotDefined();
        }
    },

    throwIfExpressionMissing: function (options) {
        if (!options.expression) {
            throw new Bifrost.validation.MissingExpression();
        }
    },

    validate: function (value, options) {
        this.throwIfOptionsUndefined(options);
        this.throwIfExpressionMissing(options);

        return (value.match(options.expression) == null) ? false : true;
    }
};

if (typeof ko !== 'undefined') {
    ko.bindingHandlers.command = {
        init: function (element, valueAccessor, allBindingAccessor, viewModel) {
            ko.applyBindingsToNode(element, { click: valueAccessor().execute }, viewModel);
        }
    };
}
Bifrost.namespace("Bifrost.commands");
Bifrost.commands.CommandResult = (function () {
    function CommandResult(existing) {
        if (typeof existing !== "undefined") {
            Bifrost.extend(this, existing);
        } else {
            this.CommandName = "";
            this.CommandId = Bifrost.Guid.create();
            this.ValidationResult = [];
            this.Success = true;
            this.Invalid = false;
            this.Exception = undefined;
        }
    }

    return {
        create: function () {
            var commandResult = new CommandResult();
            return commandResult;
        },
        createFrom: function (json) {
            var existing = $.parseJSON(json);
            var commandResult = new CommandResult(existing);
            return commandResult;
        }
    }
})();
Bifrost.namespace("Bifrost.commands");
Bifrost.commands.Command = (function (window) {
    function Command(options) {
        var self = this;
        this.name = options.name;
        this.hasError = false;
        this.isBusy = ko.observable(false);
        this.canExecute = ko.observable(true);
        this.id = Bifrost.Guid.create();
        this.result = Bifrost.commands.CommandResult.create();
        this.hasExecuted = false;
        this.successfullyExcecuted = function () {
            if (self.hasExecuted) {
                return self.result.Success === true;
            }
        };

        this.options = {
            beforeExecute: function () {
            },
            error: function () {
            },
            success: function () {
            },
            complete: function () {
            }
        };
        // Todo: add an overrideWith or similar that will always pick the one that is not undefined!
        // add validation check for "type" based on source, if not function for instance in the merger
        // exception!
        Bifrost.extend(this.options, options);

        this.parameters = options.parameters || {};


        this.initialize = function () {
            if (typeof self.viewModel === "undefined") {
                self.viewModel = window;
            }

            Bifrost.validation.validationService.applyForCommand(self);

            self.parametersAreValid = ko.computed(function () {
                for (var property in this.parameters) {
                    if (this.parameters[property].validator.isValid() == false) {
                        return false;
                    }
                }
                return true;
            }, self);
        };

        this.validate = function () {
            for (var property in self.parameters) {
				if( self.parameters[property].validator ) {
                	self.parameters[property].validator.validate(self.parameters[property]());
				}
            }
        }

        this.execute = function () {
            self.hasError = false;

            self.validate();
            if (!self.parametersAreValid()) {
                return;
            }

            self.onBeforeExecute();



            if (!self.canExecute.call(self.viewModel)) {
                return;
            }
            self.isBusy(true);

            Bifrost.commands.commandCoordinator.handle(self, {
                error: function (e) {
                    self.onError(e);
                },
                complete: function () {
                    self.onComplete();
                }
            });
        };

        this.onBeforeExecute = function () {
            self.options.beforeExecute.call(self.viewModel, self);
        };

        this.onError = function () {
            self.hasError = true;
            self.options.error.call(self.viewModel, self.result);
        };

        this.onSuccess = function () {
            self.hasError = false;
            self.options.success.call(self.viewModel, self.result);
        };

        this.onComplete = function () {
            if (!self.hasError) {
                self.options.complete.call(self.viewModel, self.result);
            }
            self.isBusy(false);
        };
    }

    return {
        create: function (options) {
            var command = new Command(options);
            command.initialize();
            return command;
        }
    };
})(window);

Bifrost.namespace("Bifrost.commands");
Bifrost.commands.CommandDescriptor = (function () {
    function CommandDescriptor(name, id, commandParameters) {
        this.Name = name;
		var commandContent = {
			Id: id
		};
		for( var property in commandParameters ) {
			commandContent[property] = commandParameters[property];
		}
        this.Command = ko.toJSON(commandParameters);
    };

    return {
        createFrom: function (command) {
            var commandDescriptor = new CommandDescriptor(command.name, command.id, command.parameters);
            return commandDescriptor;
        }
    }
})();

Bifrost.namespace("Bifrost.commands");
Bifrost.commands.commandCoordinator = (function () {
    var baseUrl = "/CommandCoordinator";
    function sendToHandler(url, data, completeHandler) {
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'json',
            data: data,
            contentType: 'application/json; charset=utf-8',
            complete: completeHandler
        });
    }

    function handleCommandCompletion(jqXHR, command, commandResult) {
        if (jqXHR.status === 200) {
            command.result = commandResult;
            command.hasExecuted = true;
            if (command.result.success === true) {
                command.onSuccess();
            } else {
                command.onError();
            }
        } else {
            command.result.success = false;
            command.result.exception = {
                Message: jqXHR.responseText,
                details: jqXHR
            };
            command.onError();
        }
        command.onComplete();
    }

    return {
        handle: function (command) {
            var methodParameters = {
                commandDescriptor: JSON.stringify(Bifrost.commands.CommandDescriptor.createFrom(command))
            };

            sendToHandler(baseUrl + "/Handle", JSON.stringify(methodParameters), function (jqXHR) {
                var commandResult = Bifrost.commands.CommandResult.createFrom(jqXHR.responseText);
                handleCommandCompletion(jqXHR, command, commandResult);
            });
        },
        handleForSaga: function (saga, commands) {
            var commandDescriptors = [];
            $.each(commands, function (index, command) {
                command.onBeforeExecute();
                commandDescriptors.push(Bifrost.commands.CommandDescriptor.createFrom(command));
            });

            var methodParameters = {
                sagaId: "\"" + saga.Id + "\"",
                commandDescriptors: JSON.stringify(commandDescriptors)
            };

            sendToHandler(baseUrl + "/HandleForSaga", JSON.stringify(methodParameters), function (jqXHR) {
                var commandResultArray = $.parseJSON(jqXHR.responseText);

                $.each(commandResultArray, function (commandResultIndex, commandResult) {
                    $.each(commands, function (commandIndex, command) {
                        if (command.id === commandResult.CommandId) {
                            handleCommandCompletion(jqXHR, command, commandResult);
                            return false;
                        }
                    });
                });
            });
        }
    };
})();

Bifrost.namespace("Bifrost.sagas");
Bifrost.sagas.Saga = (function () {
    function Saga() {
        var self = this;

        this.executeCommands = function (commands) {
            Bifrost.commands.commandCoordinator.handleForSaga(self, commands, {
                error: function (e) {
                },
                complete: function (e) {
                }
            });
        }
    }

    return {
        create: function (configuration) {
            var saga = new Saga();
            Bifrost.extend(saga, configuration);
            return saga;
        }
    }
})();

Bifrost.namespace("Bifrost.sagas");
Bifrost.sagas.sagaNarrator = (function () {
    var baseUrl = "/SagaNarrator";
    // Todo : abstract away into general Service code - look at CommandCoordinator.js for the other copy of this!s
    function post(url, data, completeHandler) {
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'json',
            data: data,
            contentType: 'application/json; charset=utf-8',
            complete: completeHandler
        });
    }

    function isRequestSuccess(jqXHR, commandResult) {
        if (jqXHR.status === 200) {
            if (commandResult.Success === true) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
        return true;
    }

    return {
        conclude: function (saga, success, error) {
            var methodParameters = {
                sagaId: "\"" + saga.Id + "\""
            };
            post(baseUrl + "/Conclude", JSON.stringify(methodParameters), function (jqXHR) {
                var commandResult = Bifrost.commands.CommandResult.createFrom(jqXHR.responseText);
                var isSuccess = isRequestSuccess(jqXHR, commandResult);
                if (isSuccess == true && typeof success === "function") {
                    success(saga);
                }
                if (isSuccess == false && typeof error === "function") {
                    error(saga);
                }
            });
        }
    }
})();

Bifrost.namespace("Bifrost.features");
Bifrost.features.UriMapping = (function () {
    function throwIfNotString(input, message) {
        if( typeof input !== "string" ) {
            throw {
                name: "ArgumentError",
                message: message
            }
        }
    }

    function UriMapping(uri, mappedUri, isDefault) {
        throwIfNotString(uri, "Missing uri for UriMapping");
        throwIfNotString(mappedUri, "Missing mappedUri for UriMapping");

        var uriComponentRegex = /\{[a-zA-Z]*\}/g
        var components = uri.match(uriComponentRegex) || [];
        var uriRegex = new RegExp(uri.replace(uriComponentRegex, "([\\w.]*)"));

        this.uri = uri;
        this.mappedUri = mappedUri;
        this.isDefault = isDefault || false;

        this.matches = function (uri) {
            var match = uri.match(uriRegex);
            if (match) {
                return true;
            }
            return false;
        }

        this.resolve = function (uri) {
            var match = uri.match(uriRegex);
            var result = mappedUri;
            $.each(components, function (i, c) {
                result = result.replace(c, match[i + 1]);
            });

            return result;
        }
    }

    return {
        create: function (uri, mappedUri, isDefault) {
            var uriMapping = new UriMapping(uri, mappedUri, isDefault);
            return uriMapping;
        }
    }
})();

Bifrost.namespace("Bifrost.features");
Bifrost.features.uriMapper = (function () {
    var mappings = new Array();

    return {
        clear: function () {
            mappings = new Array();
        },

        add: function (uri, mappedUri, isDefault) {
            var uriMapping = Bifrost.features.UriMapping.create(uri, mappedUri, isDefault);
            mappings.push(uriMapping);
        },

        getUriMappingFor: function (uri) {
            var found;
            $.each(mappings, function (i, m) {
                if (m.matches(uri)) {
                    found = m;
                    return false;
                }
            });

            if (typeof found !== "undefined") {
                return found;
            }

            throw {
                name: "ArgumentError",
                message: "URI (" + uri + ") could not be mapped"
            }
        },

        resolve: function (uri) {
            try {
                var uriMapping = Bifrost.features.uriMapper.getUriMappingFor(uri);
                return uriMapping.resolve(uri);
            } catch (e) {
                return "";
            }
        },

        allMappings: function () {
            var allMappings = new Array();
            allMappings = allMappings.concat(mappings);
            return allMappings;
        }
    }
})();
Bifrost.namespace("Bifrost.features");
Bifrost.features.ViewModel = (function () {
    function ViewModel(definition, isSingleton) {
        var self = this;
        this.definition = definition;
        this.isSingleton = isSingleton;

        this.getInstance = function () {

            if (self.isSingleton) {
                if (!self.instance) {
                    self.instance = new self.definition();
                }

                return self.instance;
            }

            return new self.definition();
        };
    }

    return {
        create: function (definition, isSingleton) {
            var viewModel = new ViewModel(definition, isSingleton);
            return viewModel;
        }
    }
})();
Bifrost.namespace("Bifrost.features");
Bifrost.features.Feature = (function () {
    function Feature(name, path, isDefault) {
        var self = this;
        this.loaded = false;
        this.renderTargets = [];
        this.name = name;
        this.path = path;
        this.isDefault = isDefault;

        if (isDefault) {
            this.viewPath = path + "/view.html";
            this.viewModelpath = path + "/viewModel.js";
        } else {
            this.viewPath = path + ".html";
            this.viewModelpath = path + ".js";
        }

        this.load = function () {
            var actualViewPath = "text!" + self.viewPath + "!strip";
            var actualViewModelPath = self.viewModelpath;

            require([actualViewPath, actualViewModelPath], function (v) {
                self.view = v;

                $.each(self.renderTargets, function (i, r) {
                    self.actualRenderTo(r);
                });

                self.renderTargets = [];

                self.loaded = true;
            });
        }

        this.defineViewModel = function (viewModel, isSingleton) {
            self.viewModel = Bifrost.features.ViewModel.create(viewModel, isSingleton);
        }

        this.renderTo = function (target) {
            if (self.loaded === false) {
                self.renderTargets.push(target);
            } else {
                self.actualRenderTo(target);
            }
        }

        this.actualRenderTo = function (target) {
            $(target).append(self.view);
            Bifrost.features.featureManager.hookup(function (a) { return $(a, $(target)); });
            var viewModel = self.viewModel.getInstance();
            ko.applyBindings(viewModel, target);
        }
    }

    return {
        create: function (name, path, isDefault) {
            var feature = new Feature(name, path, isDefault);
            feature.load();
            return feature;
        }
    }
})();

Bifrost.namespace("Bifrost.features");
Bifrost.features.featureManager = (function () {
    var allFeatures = {};

    return {
        get: function (name) {
            name = name.toLowerCase();

            if (typeof allFeatures[name] !== "undefined") {
                return allFeatures[name];
            }

            var uriMapping = Bifrost.features.uriMapper.getUriMappingFor(name);
            var path = uriMapping.resolve(name);
            var feature = Bifrost.features.Feature.create(name, path, uriMapping.isDefault);
            allFeatures[name] = feature;
            return feature;
        },
        hookup: function ($) {
            $("*[data-feature]").each(function () {
                var target = $(this);
                var name = $(this).attr("data-feature");
                var feature = Bifrost.features.featureManager.get(name);
                feature.renderTo(target[0]);
            });
        },
        all: function () {
            return allFeatures;
        }
    }
})();
(function ($) {
    $(function () {
        Bifrost.features.featureManager.hookup($);
    });
})(jQuery);



if (typeof ko !== 'undefined') {
    ko.bindingHandlers.navigateTo = {
        init: function (element, valueAccessor, allBindingAccessor, viewModel) {
            ko.applyBindingsToNode(element, { 
				click: function() {
					var featureName = valueAccessor()();
					History.pushState({feature:featureName},$(element).attr("title"),"?feature="+featureName);
				} 
			}, viewModel);
        }
    };
}

(function() {
	var container = $("[data-navigation-container]")[0];

	History.Adapter.bind(window,"statechange", function() {
		var state = History.getState();
		var featureName = state.data.feature;

		$(container).html("");
		var feature = Bifrost.features.featureManager.get(featureName);
		feature.renderTo(container);
	});

	$(function () {
		var state = History.getState();
		var hash = Bifrost.hashString.decode(state.hash);
		var featureName = hash.feature;
		if( typeof featureName !== "undefined") {
			$(window).trigger("statechange");
		} else {
			var optionString = $(container).data("navigation-container");
			var optionsDictionary = ko.jsonExpressionRewriting.parseObjectLiteral(optionString);
			$.each(optionsDictionary, function(index, item) {
				if( item.key === "default") {
					var feature = Bifrost.features.featureManager.get(item.value);
					feature.renderTo(container);
					return;
				}
			});
		}
	});
})();
Bifrost.namespace("Bifrost.messaging");
Bifrost.messaging.messenger = (function() {
	var subscribers = [];
	
	return {
		publish: function(message) {
			var messageTypeName = message.constructor.name;
			if( subscribers.hasOwnProperty(messageTypeName)) {
				$.each(subscribers[messageTypeName].subscribers, function(index, item) {
					item(message);
				});
			}
		},
	
		subscribeTo: function(messageType, subscriber) {
			var subscribersByMessageType;
			
			if( subscribers.hasOwnProperty(messageType)) {
				subscribersByMessageType = subscribers[messageType];
			} else {
				subscribersByMessageType = {subscribers:[]};
				subscribers[messageType] = subscribersByMessageType;
			}
			
			subscribersByMessageType.subscribers.push(subscriber);
		}
	}
})();
/*
@depends utils/namespace.js
@depends utils/extend.js
@depends utils/guid.js
@depends utils/isNumber.js
@depends utils/hashString.js
@depends validation/exceptions.js
@depends validation/ruleHandlers.js
@depends validation/Rule.js
@depends validation/Validator.js
@depends validation/validationMessageFor.js
@depends validation/validation.js
@depends validation/validationService.js
@depends validation/required.js
@depends validation/minLength.js
@depends validation/maxLength.js
@depends validation/range.js
@depends validation/lessThan.js
@depends validation/greaterThan.js
@depends validation/email.js
@depends validation/regex.js
@depends commands/bindingHandlers.js
@depends commands/CommandResult.js
@depends commands/Command.js
@depends commands/CommandDescriptor.js
@depends commands/commandCoordinator.js
@depends sagas/Saga.js
@depends sagas/sagaNarrator.js
@depends features/UriMapping.js
@depends features/uriMapper.js
@depends features/ViewModel.js
@depends features/Feature.js
@depends features/featureManager.js
@depends features/loader.js
@depends navigation/navigateTo.js
@depends navigation/navigation.js
@depends messaging/messenger.js
*/
