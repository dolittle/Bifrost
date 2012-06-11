Bifrost.namespace("Bifrost.sagas");
Bifrost.sagas.Saga = (function () {
    function Saga() {
        var self = this;

        this.Id = Bifrost.Guid.empty;

        this.executeCommands = function (commands) {

            var canExecuteSaga = true;

            $.each(commands, function (index, command) {
                if (command.onBeforeExecute() === false) {
                    canExecuteSaga = false;
                    return false;
                }
            });

            if (canExecuteSaga === false) {
                return;
            }
            Bifrost.commands.commandCoordinator.handleForSaga(self, commands);
        };

        this.createCommandExecutor = function (options) {
            if ($.isArray(options)) {
                options = {commands: options };
            }
            options.sagaId = self.Id;
            return Bifrost.sagas.SagaCommandExecutor.create(options);
        };
    }

    return {
        create: function (configuration) {
            var saga = new Saga();
            Bifrost.extend(saga, configuration);
            return saga;
        }
    };
})();
