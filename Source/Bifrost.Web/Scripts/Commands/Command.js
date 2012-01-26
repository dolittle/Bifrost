if (typeof ko === 'undefined') {
    throw "Requires Knockout.js";
}
Bifrost.namespace("Bifrost.commands");
Bifrost.commands.Command = (function () {
    function Command() {
        var self = this;
        this.hasError = false;
        this.isBusy = ko.observable();
        this.canExecute = ko.observable(true);
        this.id = Bifrost.guid();

        this.initialize = function () {
            if (typeof self.viewModel === "undefined") {
                self.viewModel = window;
            }
        }

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
        }

        this.onBeforeExecute = function () {
            if (typeof self.beforeExecute === "function") {
                self.beforeExecute.call(self.viewModel, self);
            }
        }

        this.onError = function (e) {
            self.hasError = true;

            if (typeof self.error === "function") {
                self.error.call(self.viewModel);
            }
        }

        this.onComplete = function () {
            if (!self.hasError && typeof self.success === "function") {
                self.success.call(self.viewModel);
            }
            self.isBusy(false);
        }
    }

    return {
        create: function (configuration) {
            var command = new Command();
            Bifrost.extend(command, configuration);
            command.initialize();
            return command;
        }
    }
})();
