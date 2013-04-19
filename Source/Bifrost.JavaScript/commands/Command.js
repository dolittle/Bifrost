Bifrost.namespace("Bifrost.commands", {
    Command: Bifrost.Type.extend(function (commandCoordinator, commandValidationService, options) {
        var self = this;
        this.name = "";
        this.targetCommand = this;
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
        this.errorCallbacks = [];
        this.successCallbacks = [];
        this.completeCallbacks = [];


        this.commandCoordinator = commandCoordinator;
        this.commandValidationService = commandValidationService;

        this.options = {
            beforeExecute: function () { },
            error: function () { },
            success: function () { },
            complete: function () { },
            properties: {}
        };

        this.error = function (callback) {
            self.errorCallbacks.push(callback);
        };
        this.success = function (callback) {
            self.successCallbacks.push(callback);
        };
        this.complete = function (callback) {
            self.completeCallbacks.push(callback);
        };

        this.setOptions = function (options) {
            Bifrost.extend(self.options, options);
            if (typeof options.name !== "undefined" && typeof options.name === "string") {
                self.name = options.name;
            }
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

                    if (!ko.isObservable(propertyValue) &&
                         (typeof propertyValue != "object" || Bifrost.isArray(propertyValue))) {

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

            $.each(self.errorCallbacks, function (index, callback) {
                callback(commandResult);
            });
        };

        this.onSuccess = function (commandResult) {
            self.options.success(commandResult);

            $.each(self.successCallbacks, function (index, callback) {
                callback(commandResult);
            });
        };

        this.onComplete = function (commandResult) {
            self.options.complete(commandResult);

            $.each(self.completeCallbacks, function (index, callback) {
                callback(commandResult);
            });
        };

        this.handleCommandResult = function (commandResult) {
            self.isBusy(false);
            if (typeof commandResult.commandValidationMessages !== "undefined") {
                self.validationMessages(commandResult.commandValidationMessages);
            }

            if (commandResult.success === false || commandResult.invalid === true) {
                if (commandResult.invalid && typeof commandResult.validationResults !== "undefined") {
                    self.commandValidationService.applyValidationResultToProperties(self.targetCommand, commandResult.validationResults);
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
                self.commandCoordinator.handle(self.targetCommand).continueWith(function (commandResult) {
                    self.handleCommandResult(commandResult);
                });
            } else {
                var commandResult = self.getCommandResultFromValidationResult(validationResult);
                self.handleCommandResult(commandResult);
            }
        };


        this.onCreated = function (lastDescendant) {
            self.targetCommand = lastDescendant;
            if (typeof options !== "undefined") {
                this.setOptions(options);
                this.copyPropertiesFromOptions(lastDescendant);
            }
            this.makePropertiesObservable(lastDescendant);
            if (typeof lastDescendant.name !== "undefined" && lastDescendant.name != "") {
                var validators = commandValidationService.applyRulesTo(lastDescendant);
                if (Bifrost.isArray(validators) && validators.length > 0) self.validators(validators);
            }
        };
    })
});