Bifrost.namespace("Bifrost.sagas");
Bifrost.sagas.SagaCommandExecutor = (function () {
    function SagaCommandExecutor(options) {
        var self = this;

        this.isBusy = ko.observable(false);
        this.sagaId = options.sagaId || Bifrost.Guid.empty;
        this.commands = options.commands || [];
        
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


        this.execute = function () {



            if (self.onBeforeExecute() === false) {
                return;
            }

            Bifrost.commands.commandCoordinator.handleForSaga(self, self.commands, {
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

            self.validate();
            if (!self.parametersAreValid()) {
                return false;
            }

            self.options.beforeExecute.call(self.viewModel, self);

            if (!self.canExecute.call(self.viewModel)) {
                return false;
            }
            self.isBusy(true);
            self.id = Bifrost.Guid.create();

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
            Bifrost.extend(sagaCommandExecutor, options);
            return sagaCommandExecutor;
        }
    };
})();
