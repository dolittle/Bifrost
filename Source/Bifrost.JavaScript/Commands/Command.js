if (typeof ko === 'undefined') {
    throw "Requires Knockout.js";
}
Bifrost.namespace("Bifrost.commands");
Bifrost.commands.Command = (function () {
    function Command(options) {
        var self = this;
        this.hasError = false;
        this.isBusy = ko.observable();
        this.canExecute = ko.observable(true);
        this.id = Bifrost.guid();
        this.options = {
            beforeExecute: function () {
            },
            error: function () {
            },
            success: function () {
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
            self.options.beforeExecute.call(self.viewModel, self);
        }

        this.onError = function (e) {
            self.hasError = true;
            self.error.call(self.viewModel);
        }

        this.onComplete = function () {
            if (!self.hasError) {
                self.success.call(self.viewModel);
            }
            self.isBusy(false);
        }
    }

    return {
        create: function (options) {
            var command = new Command(options);
            command.initialize();
            return command;
        }
    }
})();
