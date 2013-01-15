Bifrost.namespace("Bifrost.commands", {
    Command: Bifrost.Type.extend(function (commandCoordinator, commandValidationService, options) {
        var self = this;
        this.validators = ko.observableArray();
        this.validationMessages = ko.observableArray();
        this.isBusy = ko.observable(false);
        this.isValid = ko.computed(function () {
            var success = true;
            $.each(self.validators(), function (index, validator) {
                if (ko.isObservable(validator.isValid) && validator.isValid() == false) {
                    success = false;
                    return false;
                }
            });

            if (self.validationMessages().length > 0) {
                return false;
            }

            return success;
        });
        this.canExecute = ko.computed(function () {
            return self.isValid();
        });


        this.commandCoordinator = commandCoordinator;
        this.commandValidationService = commandValidationService;

        this.options = {
            beforeExecute: function () { },
            error: function () { },
            success: function () { },
            complete: function () { },
            properties: {}
        };

        this.setOptions = function (options) {
            Bifrost.extend(self.options, options);
        };

        this.copyPropertiesFromOptions = function (lastDescendant) {
            for (var property in lastDescendant.options.properties) {
                var value = lastDescendant.options.properties[property];
                if (!ko.isObservable(value)) {
                    value = ko.observable(value);
                }

                lastDescendant[property] = value;
            }
        };

        this.makePropertiesObservable = function (lastDescendant) {
            for (var property in lastDescendant) {
                if (lastDescendant.hasOwnProperty(property) && !self.hasOwnProperty(property)) {
                    var value = null;
                    var propertyValue = lastDescendant[property];
                    if (!ko.isObservable(propertyValue)) {
                        if (typeof propertyValue !== "function") {
                            if (Bifrost.isArray(propertyValue)) {
                                value = ko.observableArray(propertyValue);
                            } else {
                                value = ko.observable(propertyValue);
                            }
                            lastDescendant[property] = value;
                        }
                    }
                }
            }
        };

        this.onBeforeExecute = function () {
            self.options.beforeExecute();
        };

        this.onError = function (commandResult) {
            self.options.error(commandResult);
        };

        this.onSuccess = function (commandResult) {
            self.options.success(commandResult);
        };

        this.onComplete = function (commandResult) {
            self.options.complete(commandResult);
        };

        this.handleCommandResult = function (commandResult) {
            self.isBusy(false);
            if (typeof commandResult.commandValidationMessages !== "undefined") {
                self.validationMessages(commandResult.commandValidationMessages);
            }

            if (commandResult.success === false || commandResult.invalid === true) {
                if (commandResult.invalid && typeof commandResult.validationResults !== "undefined") {
                    self.commandValidationService.applyValidationResultToProperties(self, commandResult.validationResults);
                }
                self.onError(commandResult);
            } else {
                self.onSuccess(commandResult);
            }
            self.onComplete(commandResult);
        };

        this.getCommandResultFromValidationResult = function (validationResult) {
            var result = Bifrost.commands.CommandResult.create();
            result.invalid = true;
            return result;
        };

        this.execute = function () {
            self.isBusy(true);
            self.onBeforeExecute();
            var validationResult = self.commandValidationService.validate(this);
            if (validationResult.valid === true) {
                self.commandCoordinator.handle(self).continueWith(function (commandResult) {
                    self.handleCommandResult(commandResult);
                });
            } else {
                var commandResult = self.getCommandResultFromValidationResult(validationResult);
                self.handleCommandResult(commandResult);
            }
        };


        this.onCreated = function (lastDescendant) {
            this.setOptions(options);
            this.copyPropertiesFromOptions(lastDescendant);
            this.makePropertiesObservable(lastDescendant);
            var validators = commandValidationService.applyRulesToProperties(lastDescendant);
            if (Bifrost.isArray(validators) && validators.length > 0) self.validators(validators);
        };
    })
});

/*
Bifrost.commands.Command = (function (window) {
    function Command(options) {
        var self = this;
        this.name = options.name;
        this.hasError = false;
        this.isBusy = ko.observable(false);
        this.canExecute = ko.observable(true);
        this.id = Bifrost.Guid.create();
        this.result = Bifrost.commands.CommandResult.create();
        this.validatorsList = [];
        this.successfullyExcecuted = function () {
            if (self.hasResult()) {
                return self.result.success === true;
            }
        };

        this.hasResult = function () {
            return typeof self.result !== "undefined" && !self.result.isEmpty();
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

            self.parametersAreValid = ko.observable(true);
        };

        this.validator = Bifrost.validation.Validator.create({ required: true });

        this.updateParametersAreValid = function () {
            for (var property in this.validatorsList) {
                if (this.validatorsList[property].validator &&
					this.validatorsList[property].validator.isValid() == false) {
                    self.parametersAreValid(false);
                    return;
                }
            }
            for (var parameter in self.parameters) {
                if (self.parameters.hasOwnProperty(parameter)) {
                    if (ko.isObservable(self.parameters[parameter]) && typeof self.parameters[parameter].validator != "undefined") {
                        if (self.parameters[parameter].validator.isValid() == false) {
                            self.parametersAreValid(false);
                            return;
                        }
                    }
                }
            }

            self.parametersAreValid(true);
        };

        this.validate = function () {
            self.validator.validate(true);
            if (self.validator.isValid()) {
                for (var parameter in self.parameters) {
                    if (self.parameters.hasOwnProperty(parameter)) {
                        if (ko.isObservable(self.parameters[parameter]) && typeof self.parameters[parameter].validator != "undefined") {
                            var value = ko.utils.unwrapObservable(self.parameters[parameter]);
                            self.parameters[parameter].validator.validate(value);
                        }
                    }
                }

                for (var property in self.validatorsList) {
                    if (self.validatorsList[property].validator) {
                        self.validatorsList[property].validator.validate(self.validatorsList[property]());
                    }
                }
            }
            self.updateParametersAreValid();
        };

        this.applyValidationMessageToMembers = function (members, message) {
            for (var j = 0; j < members.length; j++) {

                var path = members[j].split(".");
                var member = self.parameters;
                for (var i in path) {
                    var step = path[i];
                    step = step.charAt(0).toLowerCase() + step.substring(1);
                    if (step in member) {
                        member = member[step];
                    } else {
                        throw "Error applying validation results: " + step + " is not a member of " + member + " (" + rule + ")";
                    }
                }


                //TODO: split on . and find object in parameters object //DONE
                if (typeof message === "string" && "validator" in member) {
                    member.validator.isValid(false);
                    member.validator.message(message);
                }
            }
        };

        this.applyValidationMessageToCommand = function (message) {
            self.validator.isValid(false);
            var newMessage = self.validator.message();
            newMessage = newMessage.length == 0 ? message : newMessage + ", " + message;
            self.validator.message(newMessage);
        };

        this.applyServerValidation = function (validationResults) {
            for (var i = 0; i < validationResults.length; i++) {
                var validationResult = validationResults[i];
                var message = validationResult.errorMessage;
                var memberNames = validationResult.memberNames;
                if (memberNames.length > 0) {
                    //one (or more) of the parameters has an error, so apply the error to those
                    self.applyValidationMessageToMembers(memberNames, message);
                } else {
                    //the command needs a validator we can apply this message to.
                    self.applyValidationMessageToCommand(message);
                }
            }
        };

        this.execute = function () {



            if (self.onBeforeExecute() === false) {
                return;
            }

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

            self.hasError = false;

            self.validate();
            if (!self.parametersAreValid()) {
                return false;
            }

            self.options.beforeExecute.call(self.viewModel, self);

            if (!self.canExecute.call(self.viewModel)) {
                return false;
            }
            self.isBusy(true);

            return true;
        };

        this.onError = function () {
            self.hasError = true;
            if (self.result.hasOwnProperty("validationResults")) {
                if (self.result.validationResults && typeof self.result.validationResults !== "undefined") {
                    self.applyServerValidation(self.result.validationResults);
                }
            }
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
})(window);*/