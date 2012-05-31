Bifrost.namespace("Bifrost.sagas");
Bifrost.sagas.Saga = (function () {
    function Saga() {
        var self = this;

        this.Id = Bifrost.Guid.empty;

        /*this.executeCommands = function (commands) {
            Bifrost.commands.commandCoordinator.handleForSaga(self, commands, {
                error: function (e) {
                },
                complete: function (e) {
                }
            });
        };*/

        this.createCommandExecutor = function (commands) {
            return Bifrost.sagas.SagaCommandExecutor({sagaId:self.Id, commands:commands});
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
