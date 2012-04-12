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

            //TODO: create a list of validators to loop through  //DONE
            Bifrost.validation.validationService.applyForCommand(self);

            //TODO: loop through list of validations, not parameters object //DONE
            self.parametersAreValid = ko.computed(function () {
                for (var property in this.validatorsList) {
                    if (this.validatorsList[property].validator &&
						this.validatorsList[property].validator.isValid() == false) {
                        return false;
                    }
                }
                return true;
            }, self);
        };

        this.validator = Bifrost.validation.Validator.create({ required: true });

        this.validate = function () {
            self.validator.validate(true);
            if (self.validator.isValid()) {
                //TODO: loop through list of validations, not parameters object //DONE
                for (var property in self.validatorsList) {
                    if (self.validatorsList[property].validator) {
                        self.validatorsList[property].validator.validate(self.validatorsList[property]());
                    }
                }
            }
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
