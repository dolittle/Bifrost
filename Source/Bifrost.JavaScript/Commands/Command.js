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

            self.parametersAreValid = ko.computed(function () {
                for (var property in this.parameters) {
                    if (this.parameters[property].validator &&
						this.parameters[property].validator.isValid() == false) {
                        return false;
                    }
                }
                return true;
            }, self);
        };

        this.validator = Bifrost.validation.Validator.create({ required: true });

        this.validate = function () {
            if (self.validator.validate(true)) {
                for (var property in self.parameters) {
                    if (self.parameters[property].validator) {
                        self.parameters[property].validator.validate(self.parameters[property]());
                    }
                }
            }
        };

        this.applyValidationMessageToMembers = function (members, message) {
            for (var j = 0; j < members.length; j++) {
                var member = members[j];
                member = member.charAt(0).toLowerCase() + member.substring(1);
                if (typeof message === "string" && typeof member === "string") {
                    if (self.parameters.hasOwnProperty(member)) {
                        self.parameters[member].validator.isValid(false);
                        self.parameters[member].validator.message(message);
                    }
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
                self.applyServerValidation(self.result.validationResults);
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
})(window);
