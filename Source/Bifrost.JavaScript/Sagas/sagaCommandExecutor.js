Bifrost.namespace("Bifrost.sagas");
Bifrost.sagas.SagaCommandExecutor = (function () {
    function SagaCommandExecutor(options) {
        var self = this;

        this.hasError = false;
        this.isBusy = ko.observable(false);
        this.sagaId = options.sagaId || Bifrost.Guid.empty;
        this.commands = options.commands || [];
        this.canExecute = ko.observable(true);

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



        this.initialize = function () {
            if (typeof self.viewModel === "undefined") {
                self.viewModel = window;
            }

            self.commandParametersAreValid = ko.computed(function () {
                for (var i = 0; i < self.commands.length; i++) {
                    if (self.commands[i].parametersAreValid() === false) {
                        return false;
                    }
                }
                return true;
            }, self);
        };

        this.forEachCommand = function (perform) {
            for (var i = 0; i < self.commands.length; i++) {
                var ret = perform(self.commands[i], i, self.commands);
                if (ret === false) {
                    break;
                }
            }
        };

        this.validateCommands = function () {
            self.forEachCommand(function (command) {
                command.validate();
            });
        };


        this.execute = function () {



            if (self.onBeforeExecute() === false) {
                return;
            }

            Bifrost.commands.commandCoordinator.handleForSagaCommandExecutor(self, self.commands, {
                error: function (e) {
                    self.onError(e);
                },
                complete: function () {
                    self.onComplete();
                },
                success: function () {
                    self.onSuccess();
                }
            });
        };

        this.onBeforeExecute = function () {

            self.hasError = false;

            self.validateCommands();
            if (!self.commandParametersAreValid()) {
                return false;
            }

            self.options.beforeExecute.call(self.viewModel, self);
            self.forEachCommand(function (command) {
                command.options.beforeExecute.call(command.viewModel, command);
            });

            
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
            self.resetAllValidationMessages();
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
            var sagaCommandExecutor = new SagaCommandExecutor(options);
            sagaCommandExecutor.initialize();
            return sagaCommandExecutor;
        }
    };
})();
