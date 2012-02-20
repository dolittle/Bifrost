var Bifrost = Bifrost || {};
(function(global, undefined) {
    Bifrost.namespace = function (ns) {
        var parent = global;
        var parts = ns.split('.');
        $.each(parts, function (index, part) {
            if (!parent.hasOwnProperty(part)) {
                parent[part] = {};
            }
            parent = parent[part];
        });
    };
})(window);
﻿Bifrost.namespace("Bifrost");
Bifrost.extend = function extend(destination, source) {
    var toString = Object.prototype.toString,
			            objTest = toString.call({});
    for (var property in source) {
        if (source[property] && objTest == toString.call(source[property])) {
            destination[property] = destination[property] || {};
            extend(destination[property], source[property]);
        } else {
            destination[property] = source[property];
        }
    }
    return destination;
};

﻿Bifrost.namespace("Bifrost");
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

﻿if (typeof ko !== 'undefined') {
    ko.bindingHandlers.command = {
        init: function (element, valueAccessor, allBindingAccessor, viewModel) {
            ko.applyBindingsToNode(element, { click: valueAccessor().execute }, viewModel);
        }
    };
}
﻿Bifrost.namespace("Bifrost.commands");
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
﻿Bifrost.namespace("Bifrost.commands");
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
                return self.result.Success ===  true;
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
        };

        this.execute = function () {
            self.hasError = false;

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

﻿Bifrost.namespace("Bifrost.commands");
Bifrost.commands.CommandDescriptor = (function () {
    function CommandDescriptor(name, id, commandParameters) {
        this.Name = name;

        var commandContent = {
            Id: id
        };

        for (var parameter in commandParameters) {
            if (typeof (commandParameters[parameter]) == "function") {
                commandContent[parameter] = commandParameters[parameter]();
            } else {
                commandContent[parameter] = commandParameters[parameter];
            }
        }

        this.Command = JSON.stringify(commandContent);
    };

    return {
        createFrom: function (command) {
            var commandDescriptor = new CommandDescriptor(command.name, command.id, command.parameters);
            return commandDescriptor;
        }
    }
})();

﻿Bifrost.namespace("Bifrost.commands");
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
            if (command.result.Success === true) {
                command.onSuccess();
            } else {
                command.onError();
            }
        } else {
            command.result.Success = false;
            command.result.Exception = {
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

﻿Bifrost.namespace("Bifrost.sagas");
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

﻿Bifrost.namespace("Bifrost.sagas");
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

﻿Bifrost.namespace("Bifrost.features");
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

﻿Bifrost.namespace("Bifrost.features");
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
﻿Bifrost.namespace("Bifrost.features");
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
﻿Bifrost.namespace("Bifrost.features");
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

﻿Bifrost.namespace("Bifrost.features");
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

﻿(function() {
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
/*
@depends utils/namespace.js
@depends utils/extend.js
@depends utils/guid.js
@depends utils/hashString.js
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
@depends navigation/navigationService.js
*/
